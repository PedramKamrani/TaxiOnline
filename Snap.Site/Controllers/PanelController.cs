using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Snap.Core.Generators;
using Snap.Core.Interface;
using Snap.Core.ViewModels;
using Snap.Core.ViewModels.Panel;
using Snap.Core.ViewModels.Payment;
using Snap.Data.Layer.Entities;
using System.Text;

namespace Snap.Site.Controllers
{
    [Authorize]
    public class PanelController : Controller
    {
        private IPanelService _panel;

        public PanelController(IPanelService panel)
        {
            _panel = panel;
        }
        public IActionResult Dashboard()
        {
            User user = _panel.GetUser(User.Identity.Name);

            Guid? transactID = _panel.ExistsUserTransact(user.Id);

            int status = -1;
            Guid? driverID = null;

            if (transactID != null)
            {
                Transact transact = _panel.GetUserTransact((Guid)transactID);

                status = transact.Status;
                driverID = transact.DriverId;
            }

            DashboardViewModel dashboard = new DashboardViewModel()
            {
                DriverId = driverID,
                UserId = user.Id,
                TransactId = transactID,
                Status = status
            };
            return View(dashboard);
        }
        //public async Task<IActionResult> TestApi()
        //{
        //    var client = new HttpClient();
        //    client.BaseAddress = new Uri("https://api.openweathermap.org");
        //    var response = await client.GetAsync($"/data/2.5/weather?lat=38&lon=52&appid=42322586b8ce663c2f1db0a19ecc0d72");

        //    var result = await response.Content.ReadAsStringAsync();

        //    var obj = JsonConvert.DeserializeObject<dynamic>(result);

        //    WeatherViewModel viewModel = new WeatherViewModel()
        //    {
        //        Temp = Math.Round(((float)obj.main.temp * 9 / 5 - 459.67), 2),
        //        Hum = Math.Round(((float)obj.main.humidity), 2)
        //    };

        //    return Content(viewModel.Hum + " | " + viewModel.Temp);
        //}
        public async Task<IActionResult> UserProfile()
        {
            var result = await _panel.GetUserDetailsAsync(User.Identity.Name);

            UserDetailProfileViewModel viewModel = new UserDetailProfileViewModel()
            {
                BirthDate = result.BirthDate,
                FullName = result.Fullname
            };

            ViewBag.IsSuccess = false;
            ViewBag.MyDate = DateTimeGenerators.ShamsiDate();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UserProfile(UserDetailProfileViewModel viewModel)
        {
            var result = await _panel.GetUserDetailsAsync(User.Identity.Name);

            bool myUpdate = _panel.UpdateUserDetailsProfile(result.UserId, viewModel);

            ViewBag.MyDate = viewModel.BirthDate;
            ViewBag.IsSuccess = myUpdate;
            return View(viewModel);
        }

        public IActionResult Payment() => View();


        [HttpPost]
        public async Task<IActionResult> Payment(PaymentViewModel viewModel)
        {
            UserDetail user = await _panel.GetUserDetailsAsync(User.Identity.Name);
            string orderNumber = CodeGenerators.GetOrderCode();

            var checkFactor = _panel.UpdateFactor(user.UserId, orderNumber, viewModel.Wallet);

            if (checkFactor == false)
            {
                Factor factor = new Factor()
                {
                    BankName = "",
                    Date = "",
                    Desc = "",
                    Id = CodeGenerators.GetId(),
                    OrderNumber = orderNumber,
                    Price = Convert.ToInt32(viewModel.Wallet),
                    RefNumber = "",
                    Time = "",
                    TraceNumber = "",
                    UserId = user.UserId
                };

                _panel.AddFactor(factor);
            }

            Guid factorID = _panel.GetFactorById(orderNumber);

            var payment = new ZarinpalSandbox.Payment(Convert.ToInt32(viewModel.Wallet));
            var result = payment.PaymentRequest("تراکنش جدید", "https://localhost:5000/Panel/PaymentCallBack/" + factorID);

            if (result.Result.Status == 100)
            {
                return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + result.Result.Authority);
            }

            return Redirect("/Panel/ResultPayment/" + factorID);
        }

        public async Task<IActionResult> PaymentCallBack(Guid id)
        {
            Factor factor = await _panel.GetFactor(id);
            string authority = HttpContext.Request.Query["Authority"];

            var payment = new ZarinpalSandbox.Payment(Convert.ToInt32(factor.Price));
            var result = payment.Verification(authority).Result;

            if (result.Status == 100)
            {
                _panel.UpdatePayment(id, DateTimeGenerators.ShamsiDate(), DateTimeGenerators.ShamsiTime(), "افزایش اعتبار زرین پال", "زرین پال", result.RefId.ToString(), result.RefId.ToString());
            }

            return Redirect("/Panel/ResultPayment/" + id);
        }


        public async Task<IActionResult> ResultPayment(Guid id)
        {
            var result = await _panel.GetFactor(id);

            return View(result);
        }


        // درگاه پرداخت بانک ملی
        [HttpPost]
        //public async Task<IActionResult> Payment(PaymentViewModel viewModel)
        //{
        //    UserDetail user = await _panel.GetUserDetailsAsync(User.Identity.Name);
        //    string orderNumber = CodeGenerators.GetOrderCode();

        //    var checkFactor = _panel.UpdateFactor(user.UserId, orderNumber, viewModel.Wallet);

        //    if (checkFactor == false)
        //    {
        //        Factor factor = new Factor()
        //        {
        //            BankName = null,
        //            Date = null,
        //            Desc = null,
        //            Id = CodeGenerators.GetId(),
        //            OrderNumber = orderNumber,
        //            Price = Convert.ToInt32(viewModel.Wallet),
        //            RefNumber = null,
        //            Time = null,
        //            TraceNumber = null,
        //            UserId = user.UserId
        //        };

        //        _panel.AddFactor(factor);
        //    }

        //    string merchantId = "";
        //    string terminalId = "";
        //    string merchantKey = "";

        //    string signEncodeTemplate = $"{terminalId};{orderNumber};{viewModel.Wallet}";

        //    var byteData = Encoding.UTF8.GetBytes(signEncodeTemplate);
        //    var myAlgo = SymmetricAlgorithm.Create("TripleDes");
        //    myAlgo.Mode = CipherMode.ECB;
        //    myAlgo.Padding = PaddingMode.PKCS7;

        //    var myEnc = myAlgo.CreateEncryptor(Convert.FromBase64String(merchantKey), new byte[8]);

        //    string signData = Convert.ToBase64String(myEnc.TransformFinalBlock(byteData, 0, byteData.Length));

        //    var myData = new
        //    {
        //        MerchanId = merchantId,
        //        TerminalId = terminalId,
        //        Amount = viewModel.Wallet,
        //        OrderId = orderNumber,
        //        LocalDateTime = DateTime.Now,
        //        ReturnUrl = "https://localhost:44369/Panel/CallBack",
        //        SignData = signData
        //    };

        //    var res = CallApi<RequestPaymentResult>("https://sadad.shaparak.ir/api/v0/Request/PaymentRequest", myData).Result;

        //    if (res.ResCode == 0)
        //    {
        //        return Redirect($"https://sadad.shaparak.ir/Purchase/Index?token={res.Token}");
        //    }
        //    else
        //    {
        //        return RedirectToAction("ResultPayment");
        //    }
        //}
        public async Task<T> CallApi<T>(string api, object value) where T : new()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(api);
                client.DefaultRequestHeaders.Accept.Clear();

                var json = JsonConvert.SerializeObject(value);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var w = client.PostAsync(api, content);
                w.Wait();

                HttpResponseMessage response = w.Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync();
                    result.Wait();
                    return JsonConvert.DeserializeObject<T>(result.Result);
                }

                return new T();
            }
        }
        //[HttpPost]
        //public async Task<IActionResult> Payment(PaymentViewModel wallet)
        //{
        //    UserDetail user = await _panel.GetUserDetailsAsync(User.Identity.Name);
        //    string orderNumber = CodeGenerators.GetOrderCode();
        //    var checkFactor = _panel.UpdateFactor(user.UserId, orderNumber, wallet.Wallet);

        //    if (checkFactor == false)
        //    {
        //        Factor factor = new Factor()
        //        {
        //            BankName = null,
        //            Date = null,
        //            Desc = null,
        //            Id = CodeGenerators.GetId(),
        //            OrderNumber = orderNumber,
        //            Price = Convert.ToInt32(wallet.Wallet),
        //            RefNumber = null,
        //            Time = null,
        //            TraceNumber = null,
        //            UserId = user.UserId
        //        };

        //        _panel.AddFactor(factor);
        //    }
        //    Guid factorID = _panel.GetFactorById(orderNumber);

        //    var payment = new ZarinpalSandbox.Payment(Convert.ToInt32(viewModel.Wallet));
        //    var result = payment.PaymentRequest("تراکنش جدید", "https://localhost:44369/Panel/PaymentCallBack/" + factorID);

        //    if (result.Result.Status == 100)
        //    {
        //        return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + result.Result.Authority);
        //    }

        //    return Redirect("/Panel/ResultPayment/" + factorID);

        //}

        public async Task<IActionResult> ConfirmRequest(double id, string lat1, string lat2, string lng1, string lng2)
        {
            long price = _panel.GetPriceType(id);

            var client = new HttpClient();

            client.BaseAddress = new Uri("https://api.openweathermap.org");
            var response = await client.GetAsync($"/data/2.5/weather?lat=38&lon=52&units=metric&appid=42322586b8ce663c2f1db0a19ecc0d72");

            var result = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<dynamic>(result);

            WeatherViewModel viewModel = new WeatherViewModel()
            {
                Hum = Math.Round((float)obj.main.humidity),
                Temp = Math.Round((float)obj.main.temp)
            };

            double humC = Convert.ToDouble(((viewModel.Hum) - 32) * (0.555));

            float tempPercent = _panel.GetTempPercent(viewModel.Temp);
            float humPercent = _panel.GetHumidityPercent(humC);

            price = Convert.ToInt64(price + (price * tempPercent));
            price = Convert.ToInt64(price + (price * humPercent));

            TransactViewModel priceConfirm = new TransactViewModel()
            {
                Fee = price,
                UserId = _panel.GetUserId(User.Identity.Name),

                StartLat = lat1,
                StartLng = lng1,
                EndLng = lng2,
                EndLat = lat2
            };

            return View(priceConfirm);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmRequest(TransactViewModel viewModel)
        {
            User user = _panel.GetUser(User.Identity.Name);

            bool isCash = true;

            if (user.Wallet >= viewModel.Fee)
            {
                isCash = false;
            }

            Transact transact = new Transact()
            {
                Id = CodeGenerators.GetId(),
                Date = DateTimeGenerators.ShamsiDate(),
                Discount = 0,
                DriverId = null,
                DriverRate = false,
                EndAddress = viewModel.EndAddress,
                EndLat = viewModel.EndLat,
                EndLng = viewModel.EndLng,
                EndTime = null,
                Fee = viewModel.Fee,
                IsCash = isCash,
                Rate = 0,
                StartAddress = viewModel.StartAddress,
                StartLat = viewModel.StartLat,
                StartLng = viewModel.StartLng,
                StartTime = null,
                Status = 0,
                Total = viewModel.Fee,
                UserId = viewModel.UserId
            };

            _panel.AddTransact(transact);

            return RedirectToAction("Dashboard");
        }

        public IActionResult Chat()
        {
            return View();
        }

    }
}

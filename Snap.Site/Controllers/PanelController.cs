using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Snap.Core.Generators;
using Snap.Core.Interface;
using Snap.Core.ViewModels;
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
            return View();
        }

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
                    BankName = null,
                    Date = null,
                    Desc = null,
                    Id = CodeGenerators.GetId(),
                    OrderNumber = orderNumber,
                    Price = Convert.ToInt32(viewModel.Wallet),
                    RefNumber = null,
                    Time = null,
                    TraceNumber = null,
                    UserId = user.UserId
                };

                _panel.AddFactor(factor);
            }

            Guid factorID = _panel.GetFactorById(orderNumber);

            var payment = new ZarinpalSandbox.Payment(Convert.ToInt32(viewModel.Wallet));
            var result = payment.PaymentRequest("تراکنش جدید", "https://localhost:44369/Panel/PaymentCallBack/" + factorID);

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

    }
}

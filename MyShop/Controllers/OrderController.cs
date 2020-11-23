using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShop.Data;
using MyShop.Extensions;
using MyShop.Models;
using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;

namespace MyShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly ShopContext _context;

        private readonly IWebHostEnvironment _environment;

        public OrderController(ShopContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<IActionResult> Add(int cartId)
        {
            var carts = await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .ToListAsync();
            
            var cart = carts.FirstOrDefault(c => c.Id == cartId);

            if (cart == null)
            {
                return NotFound();
            }

            var order = new Order
            {
                UserName = cart.UserName,
                Status = OrderStatus.Progress,
                ShippingAddress = new Address(),
                GrandTotal = cart.GrandTotal,
                OrderItems = cart.CartItems.Select(ci => new OrderItem
                {
                    Product = ci.Product,
                    Quantity = ci.Quantity,
                    TotalPrice = ci.TotalPrice.Value
                }).ToList()
            };
            
            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Order orderModel)
        {
            if (ModelState.IsValid)
            {
                var cart = _context.Carts.Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Product)
                    .FirstOrDefault(c => c.UserName == User.Identity.Name);

                orderModel.UserName = cart.UserName;
                orderModel.GrandTotal = cart.GrandTotal;
                orderModel.OrderItems = cart.CartItems.Select(ci => new OrderItem
                {
                    Product = ci.Product,
                    Quantity = ci.Quantity,
                    TotalPrice = ci.TotalPrice.Value
                }).ToList();

                _context.Orders.Add(orderModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Invoice), orderModel);
        }

        /*[HttpGet]
        public async Task<IActionResult> Finish(Order order)
        {
            var carts = await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .ToListAsync();

            var cart = carts.FirstOrDefault(c => c.UserName == User.Identity.Name);
            cart?.CartItems.Clear();

            _context.Update(cart);
            await _context.SaveChangesAsync();
            return View(order);
        }
        */

        [HttpGet]
        public async Task<IActionResult> Invoice(Order orderModel)
        {
            try
            {
                await ClearCart();
            }
            catch (NullReferenceException e)
            {
                return NotFound();
            }
            
            var orders = await _context.Orders
                .Include(o => o.ShippingAddress)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ToListAsync();

            var order = orders.FirstOrDefault(o => o.Id == orderModel.Id);

            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> InvoicePdf(int orderId)
        {
            var orders = await _context.Orders
                .Include(o => o.ShippingAddress)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ToListAsync();

            var order = orders.FirstOrDefault(o => o.Id == orderId);
            
            HtmlToPdfConverter htmlConverter = new HtmlToPdfConverter(HtmlRenderingEngine.WebKit);

            WebKitConverterSettings settings = new WebKitConverterSettings();

            //Set WebKit path
            settings.WebKitPath = Path.Combine(_environment.ContentRootPath, "QtBinariesWindows");

            //Assign WebKit settings to HTML converter
            htmlConverter.ConverterSettings = settings;

            //Convert URL to PDF
            PdfDocument document = htmlConverter.Convert("https://localhost:5001/Order/Invoice");
            
            MemoryStream ms = new MemoryStream();
            document.Save(ms);

            return File(ms.ToArray(), System.Net.Mime.MediaTypeNames.Application.Pdf, "Output.pdf");

            /*IronPdf.Installation.TempFolderPath = $"{_environment.ContentRootPath}/irontemp/";
            IronPdf.Installation.LinuxAndDockerDependenciesAutoConfig = true;

            var html = this.RenderViewAsync("_Invoice", order);
            var pdfRender = new IronPdf.HtmlToPdf();
            var pdfDoc = pdfRender.RenderHtmlAsPdf(html.Result);

            return File(pdfDoc.Stream.ToArray(), "application/pdf");*/
        }

        private async Task ClearCart()
        {
            var carts = await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .ToListAsync();

            var cart = carts.FirstOrDefault(c => c.UserName == User.Identity.Name);

            if (cart == null)
            {
                throw new NullReferenceException(nameof(cart));
            }
            
            cart.CartItems.Clear();

            _context.Update(cart);
            await _context.SaveChangesAsync();
        }
    }
}
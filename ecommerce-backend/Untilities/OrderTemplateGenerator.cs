﻿using EcommerceApi.Models;
using System.Linq;
using System.Text;

namespace EcommerceApi.Untilities
{
    public static class OrderTemplateGenerator
    {
        private static readonly string CompanyName = "Pixel Print Ltd. GST: 823338694 RT0001";
        private static readonly string PhoneNumbers = "Tel: 604-559-5000 Fax:604-559-5008";
        private static readonly string Website = "www.LightsAndParts.com";
        private static readonly string Note1 = "Dear Customer, to pay by cheque for an invoice, Please";
        private static readonly string Note2 = "PAY TO THE ORDER OF: PIXEL PRINT LTD.";
        private static readonly string Note3 = "Mention your invoice number on memo.Thank you.";
        private static readonly string Note4 = "All products must be installed by certified electrician. We will not be responsible for any damage caused by incorrectly connecting or improper use of the material.";
        private static readonly string Note5 = "All returns are subject to a <b>10% restocking fees.<b> We accept return and exchange up to <b>7 Days</b> after the date of purchase in new condition, not energized and original packaging with original Invoice and receipt.";
        private static readonly string Note6 = "All Customer orders: No returns-No exchange.";

        public static string GetHtmlString(Order order)
        {
            // var employees = DataStorage.GetAllEmployess();
            var sb = new StringBuilder();
            sb.Append($@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='center'>
                                   <img src='data:image/png;base64,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' />
                                </div>
                                <div class='center'>{CompanyName}</div>
                                <div class='center'>{PhoneNumbers}</div>
                                <div class='center'>{Website}</div>
                               
                                <div class='center smaller-font'>{Note1}<div class='red'>{Note2}</div></div>
                                <div class='center smaller-font'>{Note3}</div>
                                <hr/>
                                <div>LED Lights And Parts</div>
                                <div><b>Vancouver:</b> <br /> 
                                     3695 E 1st Ave, <br />
                                     Vancouver, BC V5M 1C2 <br />
                                     Phone: +1 (604) 559-5000 <br />
                                     <b>Abbotsford:</b> <br /> 
                                     33228 S Fraser Way <br />
                                     Abbotsford, BC, V2S 2B3 <br />
                                     Phone: +1 (604) 744-4474
                                </div>

                                <div class='right'>${order.Customer.CompanyName}</div>
                                <div><b>Invoice #{order.OrderId}</b></div>
                                <div>Sale Date: {order.OrderDate}</div>
                                <div>User: {order.CreatedByUserId}</div>
                                <hr class='spaceafter-30'/>
                                <h3 class='right'>{order.Status}</h3>    
                                <hr/>
                                
                                <table>
                                    <tr>

                                    </tr>");

                                        foreach (var item in order.OrderDetail)
                                        {
                                            sb.AppendFormat(@"<tr>
                                                                <td style='width:10%'>{0}</td>
                                                                <td style='width:70%'>X {1}</td>
                                                                <td style='width:10%'>${2}</td>
                                                                <td style='width:10%'>${3}</td>
                                                              </tr>", item.Amount, item.Product.ProductName, item.UnitPrice, item.TotalPrice);
                                        }
                                        sb.AppendFormat(@"<tr>
                                                        <td style='width:10%'></td>
                                                        <td style='width:70%'></td>
                                                        <td style='width:10%'>SubTotal:</td>
                                                        <td style='width:10%'>${0}</td>
                                                        </tr>", order.SubTotal);
                                        foreach (var tax in order.OrderTax)
                                        {
                                            sb.AppendFormat(@"<tr>
                                                                <td style='width:10%'></td>
                                                                <td style='width:70%'></td>
                                                                <td style='width:10%'>{0}:</td>
                                                                <td style='width:10%'>${1}</td>
                                                                </tr>", tax.Tax.TaxName, tax.TaxAmount);
                                        }
                                        sb.AppendFormat(@"<tr>
                                                        <td style='width:10%'></td>
                                                        <td style='width:70%'></td>
                                                        <td style='width:10%'><b>To Pay:</b></td>
                                                        <td style='width:10%'>${0}</td>
                                                        </tr>", order.Total);

                                        sb.AppendFormat(@"<tr>
                                                        <td style='width:10%'></td>
                                                        <td style='width:70%'></td>
                                                        <td style='width:10%'><b>Creadit Card / Debit:</b></td>
                                                        <td style='width:10%'>${0}</td>
                                                        </tr>", order.OrderPayment.Sum(p=>p.PaymentAmount));

                                        sb.AppendFormat(@"<tr>
                                                        <td style='width:10%'></td>
                                                        <td style='width:70%'></td>
                                                        <td style='width:10%'><b>Remain:</b></td>
                                                        <td style='width:10%'>${0}</td>
                                                        </tr>", order.Total - order.OrderPayment.Sum(p => p.PaymentAmount));

                                        sb.Append($@"
                                                    </table>
                                                    <hr/>

                                                    <hr class='spaceafter-30'/>
                                                    <div>Customer Copy</div>
                                                    <hr class='spaceafter-30'/>   
                                                    <div class='header'><p><b>Attention:</b>{Note4}</p></div>
                                                    <div class='header'><p><b>Store policy:</b>{Note5}</p></div>
                                                    <div class='header'><p><b>{Note6}</b></p></div>
                                                </body>
                                            </html>");

            return sb.ToString();
        }
    }
}

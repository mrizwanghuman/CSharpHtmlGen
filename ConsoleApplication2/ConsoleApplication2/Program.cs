using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
           
            Console.WriteLine("Item numbers. Please follow 12345,13245,1345");
            string numbers = Console.ReadLine();

            string[] numberArray = numbers.Split(',');
            Console.WriteLine("Discount percentage, Just use 20 for 20%Off");
            int discountPer = Int32.Parse(Console.ReadLine());

            List<string> completeUrlList = new List<string>();
            List<object> ItemsDetailss = new List<object>();
            foreach (var itemNumber in numberArray)
            {
                          
                string links = String.Format("https://www.reallygoodstuff.com/really-good-revolving-supply-organizer/p/{0}/", itemNumber);
                
                completeUrlList.Add(links);
            }

            foreach (var link in completeUrlList)
            {
                HtmlWeb web = new HtmlWeb();
                HtmlDocument document = web.Load(link);
                HtmlNode productTitle = document.DocumentNode.SelectNodes("//div[@class='prod-head-details']//h1").First();
                HtmlNode pPrice = document.DocumentNode.SelectNodes("//span[@data-saleprice]").First();
                HtmlAttribute priceP = pPrice.Attributes["data-saleprice"];
                var realprice = Convert.ToDouble(priceP.Value);

                double discount20 = realprice - (discountPer * realprice / 100);

                var discount = discount20.ToString("0.00");
                var pTittle = productTitle.InnerText;
                ItemsDetailss.Add(new { Title = pTittle, OriginalPrice = realprice, SalePrice = discount});

            }
            Console.WriteLine("///////////////////////////////////////////Getting Data Successfully///////////////////////////////////////");
            foreach (dynamic prduct in ItemsDetailss)
            {
                
                Console.BackgroundColor = ConsoleColor.Blue;
                 Console.WriteLine(prduct.Title);
                Console.WriteLine(prduct.OriginalPrice);
                Console.WriteLine(prduct.SalePrice);
                Console.ResetColor();
                Console.WriteLine("///////////////////////Never Give up///////////////////////////////////////");
                
            }



            Console.WriteLine("///////////////////////////////////////////Got Data Successfully///////////////////////////////////////");
            Console.WriteLine("///////////////////////////////////////////Now replacing Data and will save file///////////////////////////////////////");


            HtmlWeb localweb = new HtmlWeb();

            HtmlDocument savedDocument = localweb.Load(@"C:\Users\MRizwan\Documents\tgif\tgif.html");
               List<string> newImage = new List<string>();

            // replacing images
            foreach (var prnumber in numberArray)
            {
                var imageNewSrc = string.Format("https://www.reallygoodstuff.com/images/l/{0}.jpg", prnumber);
                newImage.Add(imageNewSrc);
            }

            var imagesSrcArray = savedDocument.DocumentNode.SelectNodes("(//img[@class='tgif_margin_top'])");

            //foreach (var image in imagesSrcArray)
            //{
            //    if (flag)
            //    {
            //        foreach (var imageSrc in newImage)
            //        {

            //                image.SetAttributeValue("src", imageSrc);

                       
            //            flag = false;
            //        }
            //    }
                
            //}
            var FirstImag = savedDocument.DocumentNode.SelectSingleNode("(//img[@class='tgif_margin_top'])[1]");
            FirstImag.SetAttributeValue("src", newImage[0]);

            var SecondImage = savedDocument.DocumentNode.SelectSingleNode("(//img[@class='tgif_margin_top'])[2]");

            SecondImage.SetAttributeValue("src", newImage[1]);

            var ThirdImage = savedDocument.DocumentNode.SelectSingleNode("(//img[@class='tgif_margin_top'])[3]");
            ThirdImage.SetAttributeValue("src", newImage[2]);

            var FourthImage = savedDocument.DocumentNode.SelectSingleNode("(//img[@class='tgif_margin_top'])[4]");
            FourthImage.SetAttributeValue("src", newImage[3]);

            var FirstColumnAchor = savedDocument.DocumentNode.SelectNodes("//li[@class='leftColumn']");
            HtmlNode[] FirstcolAchor = FirstColumnAchor[0].SelectNodes(".//a").ToArray();
            List<string> removergsstring = new List<string>();

            foreach (var item in completeUrlList)
            {
                var linkinkg = item.Substring("https://wwww.reallygoodstuff.com".Length);

                removergsstring.Add(linkinkg);
              
            }
            foreach (var item in removergsstring)
            {
                Console.WriteLine(item);
            }

            foreach (var achor in FirstcolAchor)
            {
               
                achor.SetAttributeValue("href", completeUrlList[0]);

               

            }

            HtmlNode[] SecondcolAchor = FirstColumnAchor[1].SelectNodes(".//a").ToArray();

            foreach (var achor in SecondcolAchor)
            {
                achor.SetAttributeValue("href", completeUrlList[1]);



            }

            var ThirdColumnAchor = savedDocument.DocumentNode.SelectNodes("//li[@class='rightColumn']");

            HtmlNode[] Thirdhor = ThirdColumnAchor[0].SelectNodes(".//a").ToArray();

            foreach (var achor in Thirdhor)
            {
                achor.SetAttributeValue("href", completeUrlList[2]);



            }

            HtmlNode[] fouthColumn = ThirdColumnAchor[1].SelectNodes(".//a").ToArray();

            foreach (var achor in fouthColumn)
            {
                achor.SetAttributeValue("href", completeUrlList[3]);



            }

            List<string> TitlesList = new List<string>();
            List<double> PriceList = new List<double>();
            List<string> SalePriceList = new List<string>();
            foreach (dynamic prduct in ItemsDetailss)
            {
                TitlesList.Add(prduct.Title);
                PriceList.Add(prduct.OriginalPrice);
                SalePriceList.Add(prduct.SalePrice);

            }

            // Title changing started

            var firstcoltitle = savedDocument.DocumentNode.SelectSingleNode("(//div[@class='product_title'])[1]//a");

            firstcoltitle.InnerHtml = TitlesList[0];




            var seccoltitle = savedDocument.DocumentNode.SelectSingleNode("(//div[@class='product_title'])[2]//a");
            
             seccoltitle.InnerHtml = TitlesList[1];
            

             //var thirdcoltitle = ThirdColumnAchor[0].SelectSingleNode("//span[@class='product_title']");

             var thirdcoltitle = savedDocument.DocumentNode.SelectSingleNode("(//div[@class='product_title'])[3]//a");
             thirdcoltitle.InnerHtml = TitlesList[2];

             //var fourthcoltitle = ThirdColumnAchor[1].SelectSingleNode("//span[@class='product_title']");
             var fourthcoltitle = savedDocument.DocumentNode.SelectSingleNode("(//div[@class='product_title'])[4]//a");

             fourthcoltitle.InnerHtml = TitlesList[3];



             // Orignal Price changing started

             var selectOrignalPrice = savedDocument.DocumentNode.SelectNodes("(//div[@class='price'])");
     

             for (int j = 0; j < selectOrignalPrice.Count; j++)
             {
                  var priceString = string.Format("Originally $ {0}", PriceList[j]);
                  selectOrignalPrice[j].InnerHtml = priceString;
             }

             var selectSalePrice = savedDocument.DocumentNode.SelectNodes("(//div[@class='sale_price'])");

             for (int rgs = 0; rgs < selectSalePrice.Count; rgs++)
             {
                 var saleString = string.Format("Sale! $ {0}", SalePriceList[rgs]);
                 selectSalePrice[rgs].InnerHtml = saleString;
             }

             Console.WriteLine("///////////////////////////////////////////Robot is done working with RGIF///////////////////////////////////////");
             Console.WriteLine("Do you want to add recommended products? Please say yes or no");

             var getRecomendedPrRes = Console.ReadLine().ToLower();

             if (getRecomendedPrRes == "yes")
             {
                 Console.WriteLine("Recommended Item numbers. Please follow 12345,13245,1345");
                 string RecommendedNum = Console.ReadLine();

                 string[] RecommendedNumArray = RecommendedNum.Split(',');
                 List<string> completeUrlListRecom = new List<string>();
                 List<object> ItemsDetailssRecomended = new List<object>();
                 foreach (var recomTtemNumber in RecommendedNumArray)
                 {

                     string Recommandedlinks = String.Format("https://www.reallygoodstuff.com/really-good-revolving-supply-organizer/p/{0}/", recomTtemNumber);

                     completeUrlListRecom.Add(Recommandedlinks);
                 }

                 foreach (var link in completeUrlListRecom)
                 {
                     HtmlWeb Recweb = new HtmlWeb();
                     HtmlDocument Recomdocument = Recweb.Load(link);
                     HtmlNode RecomproductTitle = Recomdocument.DocumentNode.SelectNodes("//div[@class='prod-head-details']//h1").First();
                     HtmlNode RecompPrice = Recomdocument.DocumentNode.SelectNodes("//span[@data-saleprice]").First();
                     HtmlAttribute RecompriceP = RecompPrice.Attributes["data-saleprice"];
                     var recrealprice = Convert.ToDouble(RecompriceP.Value);

                     double recdiscount20 = recrealprice - (discountPer * recrealprice / 100);

                     var recdiscount = recdiscount20.ToString("0.00");
                     var RecpTittle = RecomproductTitle.InnerText;
                     ItemsDetailssRecomended.Add(new { Title = RecpTittle, OriginalPrice = recrealprice, SalePrice = recdiscount });

                 }

                 Console.WriteLine("///////////////////////////////////////////Getting Data Successfully///////////////////////////////////////");
                 foreach (dynamic recprduct in ItemsDetailssRecomended)
                 {

                     Console.BackgroundColor = ConsoleColor.Blue;
                     Console.WriteLine(recprduct.Title);
                     Console.WriteLine(recprduct.OriginalPrice);
                     Console.WriteLine(recprduct.SalePrice);
                     Console.ResetColor();
                     Console.WriteLine("///////////////////////Never Give up///////////////////////////////////////");

                 }



                 Console.WriteLine("///////////////////////////////////////////Got Data Successfully///////////////////////////////////////");
                 Console.WriteLine("///////////////////////////////////////////Now replacing Data and will save file///////////////////////////////////////");


                 var RecommendedFirstColoum = savedDocument.DocumentNode.SelectNodes("//div[@class='leftColumn_recommended']");
                
                 List<string> RecnewImage = new List<string>();

                 // replacing images
                 foreach (var recprnumber in RecommendedNumArray)
                 {
                     var recimageNewSrc = string.Format("https://www.reallygoodstuff.com/images/l/{0}.jpg", recprnumber);
                     RecnewImage.Add(recimageNewSrc);
                 }
                 List<string> RecTitlesList = new List<string>();
                 List<double> RecPriceList = new List<double>();
                 List<string> RecSalePriceList = new List<string>();
                 foreach (dynamic prduct in ItemsDetailssRecomended)
                 {
                     RecTitlesList.Add(prduct.Title);
                     RecPriceList.Add(prduct.OriginalPrice);
                     RecSalePriceList.Add(prduct.SalePrice);

                 }

                 var FirstItemImagesrc = savedDocument.DocumentNode.SelectSingleNode("//li[@class='leftColumn_recommended']//img");
                 FirstItemImagesrc.SetAttributeValue("src", RecnewImage[0]);
                 var SecondItemImagesrc = savedDocument.DocumentNode.SelectSingleNode("//li[@class='content_recommended']//img");
                 SecondItemImagesrc.SetAttributeValue("src", RecnewImage[1]);
                 var ThirdItemImagesrc = savedDocument.DocumentNode.SelectSingleNode("//li[@class='rightColumn_recommended']//img");
                 ThirdItemImagesrc.SetAttributeValue("src", RecnewImage[2]);

                 var RecommendedFirstColoumAchors = savedDocument.DocumentNode.SelectNodes("//li[@class='leftColumn_recommended']//a");

                 foreach (var recachor in RecommendedFirstColoumAchors)
                 {
                     recachor.SetAttributeValue("href", completeUrlListRecom[0]);
                 }
                 RecommendedFirstColoumAchors[1].InnerHtml = RecTitlesList[0];
                 var RecommendedSecondColoumAchors = savedDocument.DocumentNode.SelectNodes("//li[@class='content_recommended']//a");
                 foreach (var recachor in RecommendedSecondColoumAchors)
                 {
                     recachor.SetAttributeValue("href", completeUrlListRecom[1]);
                 }
                 RecommendedSecondColoumAchors[1].InnerHtml = RecTitlesList[1];
                 var RecommendedThirdColoumAchors = savedDocument.DocumentNode.SelectNodes("//li[@class='rightColumn_recommended']//a");
                 foreach (var recachor in RecommendedThirdColoumAchors)
                 {
                     recachor.SetAttributeValue("href", completeUrlListRecom[2]);
                 }
                 RecommendedThirdColoumAchors[1].InnerHtml = RecTitlesList[2];
             }
             else
             {
                 var RecommendedPrductsArea = savedDocument.DocumentNode.SelectSingleNode("(//div[@id='recomProducts'])");

                 RecommendedPrductsArea.Attributes.Add("class", "displaynone");
                 Console.WriteLine("Thanks you can close the robot");
             }

            savedDocument.Save(@"C:\Users\MRizwan\Documents\tgif\new_tgif.html");
       

          
            Console.ReadLine();
        }

        

    }
}
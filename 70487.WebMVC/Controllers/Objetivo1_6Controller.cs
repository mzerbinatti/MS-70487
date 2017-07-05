using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace _70487.WebMVC.Controllers
{
    public class Objetivo1_6Controller : Controller
    {
        //LINK to XML
        //http://www.dotnetcurry.com/linq/564/linq-to-xml-tutorials-examples

        //XSLT
        //http://www.tizag.com/xmlTutorial/xslttutorial.php
        //https://www.w3schools.com/xml/xsl_intro.asp

        // GET: Objetivo1_6
        public ActionResult Index()
        {
            return View();
        }


        #region Criação de XML

        #region Functional Construction - XElement

        /// <summary>
        /// XML Functional Construction - Functional construction is the ability to create an XML tree in a single statement. 
        /// </summary>
        /// <returns></returns>
        public ActionResult FunctionalConstruction()
        {
            XElement contatos =
                new XElement("Contatos",
                    new XElement("Contato",
                        new XElement("Nome", "Nome Numero 1",
                            new XAttribute("atributo1", "valor do atributo 1"),
                        new XElement("Telefone", "(11) 1111-1111"),
                        new XElement("Endereco",
                            new XElement("Rua", "Rua do Nome 1"),
                            new XElement("Cidade", "Cidade 1"),
                            new XElement("Estado", "Estado 1"),
                            new XElement("CEP", "11111-111")
                        )
                    )
                )
            );

            ViewBag.functionalResult = contatos;

            return View();
        }

        #endregion

        #region XDocument

        public ActionResult XDocumentConstruction()
        {
            XDocument doc = new XDocument(CriarDocumento());
            ViewBag.xdocument = doc.ToString();

            return View();
        }

        private XDocument CriarDocumento()
        {
            XDocument doc = new XDocument(
               new XDeclaration("1.0", "utf-8", "yes"),
               new XComment("XML criado através de estruturas XDocument"),
               new XElement("Contatos",
                   new XElement("Contato",
                       new XAttribute("tipo", "pessoal"),
                       new XElement("Nome", 
                            new XElement("Primeiro", "Nome 1"),
                            new XElement("Sobrenome", "Sobrenome 1")
                       ),
                       new XElement("Telefone", "(11) 1111-1111")
                   ),
                   new XElement("Contato",
                       new XAttribute("tipo", "pessoal"),
                       new XElement("Nome", "Contato Pessoal 2"),
                       new XElement("Telefone", "(11) 2222-2222")
                   ),
                   new XElement("Contato",
                       new XAttribute("tipo", "comercial"),
                       new XElement("Nome", "Contato Comercial 1"),
                       new XElement("Telefone", "(11) 91111-1111")
                   ),
                   new XElement("Contato",
                       new XAttribute("tipo", "pessoal"),
                       new XElement("Nome", "Contato Pessoal 3"),
                       new XElement("Telefone", "(11) 3333-3333")
                   ),
                   new XElement("Contato",
                       new XAttribute("tipo", "comercial"),
                       new XElement("Nome", "Contato Comercial 2"),
                       new XElement("Telefone", "(11) 92222-2222")
                   )
               )
           );


            return doc;
        }

        #endregion



        #endregion

        #region LINQ to XML

        public ActionResult LinqToXML()
        {
            XDocument doc = new XDocument(CriarDocumento());
            ViewBag.xdocument = doc.ToString(); 

            //Ancestors
            var ancestors = doc.Root.Ancestors("Contato");
            foreach(XElement el in ancestors)
            {
                ViewBag.ancestors = ViewBag.ancestors + el.Name + ": " + el.Value + "<br />";
            }
            ViewBag.ancestors = new HtmlString(ViewBag.ancestors);

            //Descendants
            var descendants = doc.Root.Descendants("Contato");
            foreach (XElement el in descendants)
            {
                ViewBag.descendants = ViewBag.descendants + el.Name + ": " + el.Value + "<br />";
            }
            ViewBag.descendants = new HtmlString(ViewBag.descendants);

            //Elements
            var elements = from m in doc.Root.Elements("Contato")
                           select m;
            foreach (XElement el in elements)
            {
                ViewBag.elements = ViewBag.elements + el.Name + ": " + el.Value + "<br />";
            }
            ViewBag.elements = new HtmlString(ViewBag.elements);

            //ElementsAfterSelf
            var elementsAfterSelf = doc.Root.ElementsAfterSelf("Contato");
            foreach (XElement el in elementsAfterSelf)
            {
                ViewBag.elementsAfterSelf = ViewBag.elementsAfterSelf + el.Name + ": " + el.Value + "<br />";
            }
            ViewBag.elementsAfterSelf = new HtmlString(ViewBag.elementsAfterSelf);

            //ElementsBeforeSelf
            var elementsBeforeSelf = doc.Root.ElementsBeforeSelf("Contato");
            foreach (XElement el in elementsBeforeSelf)
            {
                ViewBag.elementsBeforeSelf = ViewBag.elementsBeforeSelf + el.Name + ": " + el.Value + "<br />";
            }
            ViewBag.elementsBeforeSelf = new HtmlString(ViewBag.elementsBeforeSelf);

            //AncestorsAndSelf
            //DescendantsAndSelf

            return View();

        }


        #endregion
    }
}
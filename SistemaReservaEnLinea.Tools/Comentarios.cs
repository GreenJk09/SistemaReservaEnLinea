using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistemaReservaEnLinea.Tools
{
    public class Comentarios
    {
        public static string GetComentarios(int idComment, string tipo, List<SistemaReservaEnLinea.Models.Comentarios> objComOriginal, string url)
        {
            string strResultado = "";
            var objCom = objComOriginal.Where(c => c.ParentId == idComment);
            foreach (var item in objCom)
            {
                string nombre = "comentario" + item.Id.ToString();
                strResultado += "<div  id=\"" + nombre + "\" class=\"media media-middle\">" +
                                    "<div class=\"media-left\">" +
                                        "<img src=\"" + url + "\"" +
                                             "class=\"img-fluid\"" +
                                             "alt=\"media-img\" />" +
                                    "</div>" +
                                    "<div class=\"media-body\">" +
                                        "<div class=\"media-body blog_media\">" +
                                            "<h3 class=\"media-heading\">" +
                                           item.Nombre + " <span>Posted on: " + item.Fecha.ToString("dd MMM yyyy") + "</span>" +
                                            "</h3>" +
                                            "<p>" + item.Comentario + "</p>" +
                                            "<a class=\"respondermensaje\"  href=\"#\" data-responderid=\"" + item.Id + "\">" +
                                             "   <i class=\"fa fa-reply-all\" aria-hidden=\"true\"></i>" +
                                              "  Responder" +
                                            "</a>" +
                                            "<!-- Nested media object -->" +
                                        "</div>" +
                                    "</div>" +
                                "</div>";
                strResultado += GetComentarios(item.Id, tipo, objComOriginal, url);
            }
            return strResultado;
        }

        public static string GetComentario(int idComment, string tipo, SistemaReservaEnLinea.Models.Comentarios item, string url)
        {
            string strResultado = "";
            string nombre = "comentario" + item.Id.ToString();
            strResultado += "<div  id=\"" + nombre + "\" class=\"media media-middle\">" +
                                "<div class=\"media-left\">" +
                                    "<img src=\"" + url + "\"" +
                                         "class=\"img-fluid\"" +
                                         "alt=\"media-img\" />" +
                                "</div>" +
                                "<div class=\"media-body\">" +
                                    "<div class=\"media-body blog_media\">" +
                                        "<h3 class=\"media-heading\">" +
                                       item.Nombre + " <span>Posted on: " + item.Fecha.ToString("dd MMM yyyy") + "</span>" +
                                        "</h3>" +
                                        "<p>" + item.Comentario + "</p>" +
                                        "<a class=\"respondermensaje\"  href=\"#\" data-responderid=\"" + item.Id + "\">" +
                                         "   <i class=\"fa fa-reply-all\" aria-hidden=\"true\"></i>" +
                                          "  Responder" +
                                        "</a>" +
                                        "<!-- Nested media object -->" +
                                    "</div>" +
                                "</div>" +
                            "</div>";
            return strResultado;
        }
    }
}

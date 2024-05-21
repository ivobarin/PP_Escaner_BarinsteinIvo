using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    // Cada uno de los informes públicos devolverá, dado un escáner y un estado en el que deban
    // encontrarse los documentos tenidos en cuenta, los siguientes datos:

    /*
     * extensión: el total de la extensión de lo procesado según el escáner y el estado. Es
     * decir, el total de páginas en el caso de los libros y el total de cm2 en el caso de los
     * mapas.
     * 
     * cantidad: el número total de ítems únicos procesados según el escáner y el estado.
     * 
     * resumen: se muestran los datos de cada uno de los ítems contenidos en una lista
     * según el escáner y el estado.
     */

    public static class Informes
    {
        private static void MostrarDocumentosPorEstado(Escaner e, Documento.Paso estado, out int extension, out int cantidad, out string resumen)
        {
            int totalExtension = 0;
            int itemsUnicos = 0;
            string cadena = "";

            if (e.Tipo == Escaner.TipoDoc.libro)
            {
                foreach (Libro libro in e.ListaDocumentos.Cast<Libro>())
                {
                    if (libro.Estado == estado)
                    {
                        totalExtension += libro.NumPaginas;
                        itemsUnicos++;
                        cadena += libro.ToString();
                    }
                }
            }
            else if (e.Tipo == Escaner.TipoDoc.mapa)
            {
                foreach (Mapa mapa in e.ListaDocumentos.Cast<Mapa>())
                {
                    if (mapa.Estado == estado)
                    {
                        totalExtension += mapa.Superficie;
                        itemsUnicos++;
                        cadena += mapa.ToString();
                    }
                }
            }

            extension = totalExtension;
            cantidad = itemsUnicos;
            resumen = cadena;
        }

        public static void MostrarDistribuidos(Escaner e, out int extension, out int cantidad, out string resumen)
        {
            MostrarDocumentosPorEstado(e, Documento.Paso.Distribuido, out extension, out cantidad, out resumen);
        }

        public static void MostrarEnEscaner(Escaner e, out int extension, out int cantidad, out string resumen)
        {
            MostrarDocumentosPorEstado(e, Documento.Paso.EnEscaner, out extension, out cantidad, out resumen);
        }

        public static void MostrarEnRevision(Escaner e, out int extension, out int cantidad, out string resumen)
        {
            MostrarDocumentosPorEstado(e, Documento.Paso.EnRevision, out extension, out cantidad, out resumen);
        }

        public static void MostrarTerminados(Escaner e, out int extension, out int cantidad, out string resumen)
        {
            MostrarDocumentosPorEstado(e, Documento.Paso.Terminado, out extension, out cantidad, out resumen);
        }
    }
}

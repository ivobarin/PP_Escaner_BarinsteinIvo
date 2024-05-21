using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Entidades
{
    public class Escaner
    {
        // El escáner de los mapas(cama plana de gran formato) está en la mapoteca y
        // es distinto al de los libros(escáner con alimentador de páginas) el cual está en la oficina de procesos técnicos.
        public enum Departamento
        {
            nulo,
            mapoteca,
            procesosTecnicos
        }

        public enum TipoDoc
        {
            libro,
            mapa
        }

        readonly private List<Documento> listaDocumentos;
        readonly private Departamento locacion;
        readonly private string marca;
        readonly private TipoDoc tipo;

        public Escaner(string marca, TipoDoc tipo)
        {
            this.marca = marca;
            this.tipo = tipo;
            this.listaDocumentos = new List<Documento>(); // El constructor debe inicializar la lista de documentos.

            // El constructor inicializa la locación según el tipo de documento a escanear
            this.locacion = this.Tipo == TipoDoc.mapa ? Departamento.mapoteca : Departamento.procesosTecnicos;
        }

        // La sobrecarga del operador “==” comprueba si hay un documento igual en la lista. Devuelve true si encuentra, false si no.
        public static bool operator ==(Escaner e, Documento d)
        {
            bool retorno = false;
            foreach (Documento docs in e.ListaDocumentos)
            {
                // Comprueba si el tipo de documento es el mismo que el de la lista actual
                if (e.Tipo == TipoDoc.libro && d.GetType() == typeof(Libro))
                {
                    // Si los tipos coinciden, castea y verifica si los documentos son iguales
                    if ((Libro)docs == (Libro)d)
                    {
                        retorno = true;
                        break;
                    }
                }
                else if (e.Tipo == TipoDoc.mapa && d.GetType() == typeof(Mapa))
                { 
                    if ((Mapa)docs == (Mapa)d)
                    {
                        retorno = true;
                        break;
                    }
                }
            }
            return retorno;
        }

        public static bool operator !=(Escaner e, Documento d)
        {
            return !(e == d);
        }


        //El método “CambiarEstadoDocumento()” cámbiará el estado de los documentos que esten en listaDocumentos.
        public bool CambiarEstadoDocumento(Documento d)
        {
            bool retorno = false;
            /*
             * Valida que el documento se encuentre en la lista con el método Contains()
             * Valida que el estado sea distinto de Paso "Terminado"
             * Valida que el documento sea del mismo tipo que es el Escaner
             */
            if (this.ListaDocumentos.Contains(d) && d.Estado != Documento.Paso.Terminado &&
                ((d.GetType() == typeof(Libro) && this.Tipo == TipoDoc.libro) || (d.GetType() == typeof(Mapa) && this.Tipo == TipoDoc.mapa)))
            {
                d.AvanzarEstado();
                retorno = true;
            }
            return retorno;
        }

        /*
         La sobrecarga del operador “+” añade el documento a la lista de documentos en el caso de que no haya un documento igual ya en ella.
         También debe añadirlo solo si está en estado “Inicio”. Antes de añadirlo a la lista debe cambiar el estado a “Distribuido”.
         */
        public static bool operator +(Escaner e, Documento d)
        {
            bool retorno = false;

            /* Valida que no haya ningun documento igual en la lista. 
             * Valida que el estado del documento sea "Inicio"
             * Valida que el tipo de escaner tome solo el tipo el tipo de documento correspondiente.
             */
            if (e != d && d.Estado == Documento.Paso.Inicio &&
               ((e.Tipo == TipoDoc.libro && d.GetType() == typeof(Libro)) ||
               (e.Tipo == TipoDoc.mapa && d.GetType() == typeof(Mapa))))
            {
                // cambia el paso del documento a Distribuido y despues lo agrega a la lista
                d.AvanzarEstado(); 
                e.ListaDocumentos.Add(d);
                retorno = true;
            }
            return retorno;
        }

        #region Propiedades 
        public List<Documento> ListaDocumentos
        {
            get { return listaDocumentos; }
        }

        public Departamento Locacion
        {
            get { return locacion; }
        }

        public string Marca
        {
            get { return marca; }
        }

        public TipoDoc Tipo
        {
            get { return tipo; }
        }
        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Libro : Documento
    {
        readonly private int numPaginas;
        public Libro(string titulo, string autor, int anio, string numNormalizado, string barcode, int numPaginas) : base(titulo, autor, anio, numNormalizado, barcode)
        {
            this.numPaginas = numPaginas;
        }

        #region Propiedades
        public int NumPaginas { get { return this.numPaginas; } }

        // La propiedad ISBN en los libros muestra el NumNormalizado.
        public string ISBN { get { return this.NumNormalizado; } }
        #endregion

        //En el caso de libros, se considerará que son el mismo libro cuando:
        //→ Tenga el mismo barcode o
        //→ tenga el mismo ISBN o
        //→ tenga el mismo título y el mismo autor.
        public static bool operator ==(Libro l1, Libro l2)
        {
            bool retorno = false;
            if ((l1.Barcode == l2.Barcode) || (l1.ISBN == l2.ISBN) ||
                (l1.Autor == l2.Autor && l1.Titulo == l2.Titulo))
            {
                retorno = true;
            }
            return retorno;
        }

        public static bool operator !=(Libro l1, Libro l2)
        {
            return !(l1 == l2);
        }

        public override string ToString()
        {
            StringBuilder texto = new();
            texto.Append(base.ToString());
            texto.Replace("*", $"ISBN: {this.ISBN}\n");
            texto.AppendLine($"Número de páginas: {this.NumPaginas}.");
            return texto.ToString();
        }
    }
}

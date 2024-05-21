using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static Entidades.Documento;

namespace Entidades
{
    public class Mapa : Documento
    {
        readonly int ancho;
        readonly int alto;

        // Los mapas no tienen numeroNormalizado y para respetar la firma del constructor del pdf, lo coloque como un string vacio. 
        public Mapa(string titulo, string autor, int anio, string numNormalizado, string barcode, int alto, int ancho) : base(titulo, autor, anio, "", barcode)
        {
            this.ancho = ancho;
            this.alto = alto;
        }

        #region Propiedades
        public int Alto { get { return this.ancho; } }
        public int Ancho { get { return this.alto; } }
        public int Superficie { get { return (this.Alto * this.Ancho); } }
        #endregion

        // En el caso de los mapas se considerará que son el mismo mapa cuando: 
        // → Tenga el mismo barcode o 
        // → tenga el mismo título y el mismo autor y el mismo año y la misma superficie.
        public static bool operator ==(Mapa m1, Mapa m2)
        {
            bool retorno = false;
            if ((m1.Barcode == m2.Barcode) ||
                (m1.Titulo == m2.Titulo && m1.Autor == m2.Autor &&
                m1.Anio == m2.Anio && m1.Superficie == m2.Superficie))
            {
                retorno = true;
            }
            return retorno;
        }

        public static bool operator !=(Mapa m1, Mapa m2)
        {
            return !(m1 == m2);
        }

        public override string ToString()
        {
            StringBuilder texto = new();
            texto.Append(base.ToString());
            int index = (texto.ToString()).IndexOf("*");
            texto.Remove(index, 1);
            texto.AppendLine($"Superficie: {this.Ancho} * {this.Alto} = {this.Superficie} cm2.");
            return texto.ToString();
        }
    }
}

using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Entidades
{
    public abstract class Documento
    {
        // → Inicio: el valor por defecto de los documentos.
        // → Distribuido: el documento ya está en el escáner que le corresponde. 
        // → EnEscaner: el documento está siendo escaneado.
        // → EnRevision: el documento está en el paso del proceso en el que se revisa si el escaneo no tuvo fallos (problemas de pixelado, páginas faltantes…).
        // → Terminado: el documento ya ha sido escaneado y aprobado pues el proceso de revisión fue positivo.
        public enum Paso
        {
            Inicio,
            Distribuido,
            EnEscaner,
            EnRevision,
            Terminado
        }

        readonly private int anio;
        readonly private string autor;
        readonly private string barcode;
        private Paso estado; // el estado no es solo lectura debido a que AvanzarEstado() debe poder modificarlo. 
        readonly private string numNormalizado; // cadena de texto numerica antes de ser ISBN (identificador único para libros)
        readonly private string titulo;

        public Documento(string titulo, string autor, int anio, string numNormalizado, string barcode)
        {
            this.titulo = titulo;
            this.autor = autor;
            this.anio = anio;
            this.numNormalizado = numNormalizado;
            this.barcode = barcode;
            this.estado = Paso.Inicio; // El estado debe inicializarse como “Inicio”.
        }

        #region Propiedades
        public int Anio { get { return anio; } }
        public string Autor { get { return autor; } }
        public string Barcode { get { return barcode; } }
        public Paso Estado { get { return estado; } }

        //La propiedad de NumNormalizado solo debe poder verse desde las clases derivadas (protected)
        protected string NumNormalizado { get { return numNormalizado; } }
        public string Titulo { get { return titulo; } }
        #endregion

        // El método AvanzarEstado() debe pasar al siguiente estado dentro del orden que se estableció en el requerimiento.
        // Debe devolver false si el documento ya está terminado.
        public bool AvanzarEstado()
        {
            bool retorno = false;
            if (this.Estado != Paso.Terminado)
            {
                this.estado++; 
                retorno = true;      
            }
            return retorno;
        }

        // El método ToString() debe usar StringBuilder para mostrar todos los datos del documento.
        public new virtual string ToString()
        {
            StringBuilder texto = new();
            texto.AppendLine($"\nTitulo: {this.Titulo}");
            texto.AppendLine($"Autor: {this.Autor}");
            texto.AppendLine($"Año: {this.Anio}");
            texto.Append('*');
            texto.AppendLine($"Cód. de barras: {this.Barcode}");
            return texto.ToString();
        }
    }
}

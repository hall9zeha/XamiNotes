using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
namespace XamiNotes.Modelo
{

   public class MisNotas
    {
       

        [PrimaryKey, AutoIncrement ]
        public  int IdNotas { get; set; }
        public  int IdColor { get; set; }
        public int IdFont { get; set; }
        public  string Titulo { get; set; }
        public  string Contenido { get; set; }
        public  DateTime FechaNota { get; set; }
        public  DateTime Recordatorio { get; set; }
        public DateTime FechaModificacion { get; set; }
        
       

        public override string ToString()
        {
            if (Titulo == null||Titulo=="")
            {
                int cortar=0;
                int maxCar = 30;
                string contenidoCortado = "";
                if (Contenido.Length > maxCar)
                {
                    cortar = Contenido.Length-maxCar;
                    contenidoCortado = Contenido.Remove(maxCar,cortar);
                    return $"{contenidoCortado + "..."} \n {FechaNota}";
                }
                    
                else
                {
                    return $"{Contenido} \n {FechaNota}";
                }
            }
            else

            {
                
                return $"{Titulo}  \t\t\t\t\t\t   {FechaNota}"; }
            
        }

    }
   

}
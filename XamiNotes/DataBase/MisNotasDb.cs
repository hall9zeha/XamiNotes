using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using System.IO;
using XamiNotes.Modelo;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
namespace XamiNotes.DataBase
{
    public class MisNotasDb
    {
        static string nombreDb = "MisNotas.db3";
        static string pathDb = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        static string rutaDb = Path.Combine(pathDb, nombreDb);
        
        private static SQLiteConnection cn;

        public static SQLiteConnection Cn
        {

            get {
                return cn = new SQLiteConnection(rutaDb);
                
            }
            
        }
        //primero probemos este método

        public static int GuardarNota<MisNotas>(MisNotas objNota)
        {
            var i=0;
          
                Cn.CreateTable<MisNotas>();
                i = Cn.Insert(objNota);
                return i;
           


        }
        public static List<MisNotas> ListarNotas()
        {
            //using (SQLiteConnection Cnn = new SQLiteConnection(rutaDb))
           
                Cn.CreateTable<MisNotas>();
                List<MisNotas> lista = new List<MisNotas>();
                lista = Cn.Table<MisNotas>().ToList();
                return lista;
           
        }
        public static int EditarNota(MisNotas objNota)
        {
            int i = 0;
            i = Cn.Update(objNota);
            return i;
        }
        public static int EliminarNota(MisNotas objNota)
        {
            int i= 0;
            i = Cn.Delete(objNota);
            return i;


        }
            

    }
}
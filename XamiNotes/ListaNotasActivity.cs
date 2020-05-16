﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using XamiNotes.DataBase;
using XamiNotes.Modelo;

namespace XamiNotes
{
    [Activity( Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class ListaNotasActivity : Activity
    {
        List<MisNotas> listaNotas = new List<MisNotas>();
        ListView listViewNotas;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ListaNotaslayout);

            Toolbar toolbarNotas = FindViewById<Toolbar>(Resource.Id.toolbarNotas);
            listViewNotas = FindViewById<ListView>(Resource.Id.listViewNotas);
            SetActionBar(toolbarNotas);
            
            ActionBar.Title = "Mis Notas";

            listarNotas();

            //Creamos el evento click de los items del listViewNotas

            listViewNotas.ItemClick += ListViewNotas_ItemClick;
        }

        private void ListViewNotas_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //Le pasamos el parámetro de la posición del ítem seleccionado, para que lleve todo su contenido
            MostrarDetalleNota(e.Position);
            
        }

        public void listarNotas()
        {
            listaNotas = MisNotasDb.ListarNotas();
            if (listaNotas[1].IdColor == 1)
            {
                listViewNotas.SetBackgroundColor(Android.Graphics.Color.LightYellow);
                
            }
            ArrayAdapter listaNotasAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, listaNotas);
            listViewNotas.Adapter = listaNotasAdapter;
        }
        protected override void OnRestart()
        {
            
            base.OnRestart();
            listarNotas();
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            //change main_compat_menu
            MenuInflater.Inflate(Resource.Menu.MenuToolbar, menu);
            return base.OnCreateOptionsMenu(menu);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.TitleFormatted.ToString() == "Nuevo")
            {
                Intent intentNuevaNota = new Intent(this, typeof(NuevaNotaActivity));
                StartActivity(intentNuevaNota);
            }
            else
            { Toast.MakeText(this, "no implementado", ToastLength.Short).Show(); }

            

            return base.OnOptionsItemSelected(item);

           

        }
        public void MostrarDetalleNota(int posicion)
        {
            //para buscar los datos en el listview que previamente debe estar cargado con la lista, no tenemos que crear una 
            //nueva instancia de la List, solo usar la que ya está cargada en ListarNotas, y la List que esta como variable global
            
            var detalleListaNotas = listaNotas[posicion];

            Intent intentDetalleNota = new Intent(this, typeof(DetalleNotasActivity));
            Bundle bundleNota = new Bundle();
            bundleNota.PutInt("IdNotas",detalleListaNotas.IdNotas);
            bundleNota.PutInt("IdColor", detalleListaNotas.IdColor);
            bundleNota.PutString("Titulo", detalleListaNotas.Titulo);
            bundleNota.PutString("Contenido", detalleListaNotas.Contenido);
            bundleNota.PutString("FechaNota", detalleListaNotas.FechaNota.ToString());
            
            intentDetalleNota.PutExtras(bundleNota);
            StartActivity(intentDetalleNota);
          
        }

    }
}
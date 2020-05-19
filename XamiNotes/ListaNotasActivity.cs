using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Transitions;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using XamiNotes.Clases;
using XamiNotes.DataBase;
using XamiNotes.Modelo;

namespace XamiNotes
{
    [Activity( Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class ListaNotasActivity : Activity
    {
        List<MisNotas> listaNotas = new List<MisNotas>();
        
        RecyclerView.LayoutManager mLayoutManager;
        MyAdapter mAdapter;
        MisNotas objNotas = new MisNotas();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ListaNotaslayout);

            Android.Widget.Toolbar toolbarNotas = FindViewById<Android.Widget.Toolbar>(Resource.Id.toolbarNotas);

            SetActionBar(toolbarNotas);

            ActionBar.Title = "Mis Notas";

            listarNotas();

            transicionSlide();

           

        }
        //Crearemos efecto de transición
        private Slide transicionSlide()
        {
            Slide slide = new Slide();
            slide.SetDuration(3000);
            //slide.SetMode(Visibility.ModeOut);
            slide.SetInterpolator(new OvershootInterpolator());
            return slide;

        }
       

        public void listarNotas()
        {
            RecyclerView notasRecyclerView = FindViewById<RecyclerView>(Resource.Id.listaNotasRecycler);
            mLayoutManager = new LinearLayoutManager(this);
            notasRecyclerView.SetLayoutManager(mLayoutManager);
            listaNotas = MisNotasDb.ListarNotas();
            mAdapter = new MyAdapter(listaNotas);
            mAdapter.ItemClick += MAdapter_ItemClick;
            notasRecyclerView.SetAdapter(mAdapter);
            


        }

        private void MAdapter_ItemClick(object sender, int posicionNota)
        {
            int response = posicionNota;
            MostrarDetalleNota(response);
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
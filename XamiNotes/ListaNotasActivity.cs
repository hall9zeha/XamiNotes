using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
//using Android.Support.Transitions;
using Android.Transitions;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using XamiNotes.Clases;
using XamiNotes.DataBase;
using XamiNotes.Modelo;
using SearchView = Android.Widget.SearchView;
using Android.Support.V4.View;
using Android.Support.Design.Widget;

namespace XamiNotes
{
    [Activity( Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class ListaNotasActivity : Activity
    {
        Dialog popMenu;
        JavaList<MisNotas> listaNotas = new JavaList<MisNotas>();
        RecyclerView listaRecycler;
        RecyclerView.LayoutManager mLayoutManager;
        FloatingActionButton fbNuevaNota;
        MyAdapter mAdapter;
        TextView optionNota, optionLista;
        MisNotas objNotas = new MisNotas();
        Android.Widget.Toolbar toolbarNotas;
        static SearchView searchViewNotas;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ListaNotaslayout);
            listaRecycler = FindViewById<RecyclerView>(Resource.Id.listaNotasRecycler);
            toolbarNotas = FindViewById<Android.Widget.Toolbar>(Resource.Id.toolbarNotas);
            searchViewNotas = FindViewById<SearchView>(Resource.Id.action_search);
            fbNuevaNota = FindViewById<FloatingActionButton>(Resource.Id.fbNuevaNota);

            SetActionBar(toolbarNotas);

            ActionBar.Title = "Mis Notas";

            listarNotas();
            fbNuevaNota.Click += FbNuevaNota_Click;
           
            Window.EnterTransition = transicionSlide();
            Window.ExitTransition = transicionSlide();
            Window.ReturnTransition = transicionSlide();
            Window.AllowEnterTransitionOverlap = false;
            //searchViewNotas.QueryTextChange += SearchViewNotas_QueryTextChange;


        }
        //Al presionar el FloatButton se mostrará un Popup que contiene unos controles para las opciones de nota o lista de tareas
        private void FbNuevaNota_Click(object sender, EventArgs e)
        {
            popMenu = new Dialog(this);
            popMenu.SetContentView(Resource.Layout.OptionsNotePopUp);
            popMenu.Window.SetSoftInputMode(SoftInput.AdjustResize);
            //popMenu.SetCancelable(true);

            popMenu.Show();

            popMenu.Window.SetLayout(WindowManagerLayoutParams.WrapContent, WindowManagerLayoutParams.WrapContent);
            popMenu.Window.SetBackgroundDrawableResource(Android.Resource.Color.Transparent);
            optionNota = popMenu.FindViewById<TextView>(Resource.Id.textViewNota);
            optionLista = popMenu.FindViewById<TextView>(Resource.Id.textViewLista);
            optionNota.Click += OptionNota_Click;
            optionLista.Click += OptionLista_Click;
        }

        private void OptionLista_Click(object sender, EventArgs e)
        {
            popMenu.Dismiss();
            popMenu.Hide();
            Toast.MakeText(this, "No implementado aún", ToastLength.Short).Show();
        }

        private void OptionNota_Click(object sender, EventArgs e)

        {
            popMenu.Dismiss();
            popMenu.Hide();
            Intent intentNuevaNota = new Intent(this, typeof(NuevaNotaActivity));
            StartActivity(intentNuevaNota);
        }



        //**************************
        private class SearchViewExpandListener : Java.Lang.Object, MenuItemCompat.IOnActionExpandListener
        {
            private readonly IFilterable _adapter;

            public SearchViewExpandListener(IFilterable adapter)
            {
                _adapter = adapter;
            }

            public bool OnMenuItemActionCollapse(IMenuItem item)
            {
                _adapter.Filter.InvokeFilter("");
                return true;
            }

            public bool OnMenuItemActionExpand(IMenuItem item)
            {
                return true;
            }
        }
        //***************************
        private void SearchViewNotas_QueryTextChange(object sender, SearchView.QueryTextChangeEventArgs e)
        {
            mAdapter.Filter.InvokeFilter(e.NewText);
        }

        //Crearemos efecto de transición
        private Slide transicionSlide()
        {
            Android.Transitions.Slide slide = new Android.Transitions.Slide(GravityFlags.Left);
            slide.SetDuration(400);
            //slide.SetMode(Visibility.ModeOut);
            //slide.ExcludeTarget(toolbarNotas, true);
            
            slide.SetInterpolator(new DecelerateInterpolator());
            return slide;

        }
       

        public void listarNotas()
        {
            RecyclerView notasRecyclerView = FindViewById<RecyclerView>(Resource.Id.listaNotasRecycler);
            mLayoutManager = new LinearLayoutManager(this);
            notasRecyclerView.SetLayoutManager(mLayoutManager);
            listaNotas = MisNotasDb.ListarNotas();
            mAdapter = new MyAdapter(listaNotas,listaRecycler,this );
           
            notasRecyclerView.SetAdapter(mAdapter);
            


        }
        //Limpiar el proyecto y luego recompilar para que algunos recursos puedan ser agregados
       
        protected override void OnRestart()
        {
            
            base.OnRestart();
            listarNotas();
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            //change main_compat_menu
            ImageView iconsearch = (ImageView) FindViewById(Resource.Id.search_button);
            
            MenuInflater.Inflate(Resource.Menu.MenuToolbar, menu);

            //*****
            var item = menu.FindItem(Resource.Id.action_search);
            var searchView = MenuItemCompat.GetActionView(item);
            SearchView _searchView = searchView.JavaCast<Android.Widget.SearchView>();

            _searchView.QueryTextChange += (s, e) => mAdapter.Filter.InvokeFilter(e.NewText);

            _searchView.QueryTextSubmit += (s, e) =>
            {
                // Handle enter/search button on keyboard here
                Toast.MakeText(this, "Searched for: " + e.Query, ToastLength.Short).Show();
                e.Handled = true;
            };

            MenuItemCompat.SetOnActionExpandListener(item, new SearchViewExpandListener(mAdapter));



            //*****

            return base.OnCreateOptionsMenu(menu);

        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.TitleFormatted.ToString() == "Nuevo")
            {
               
                //Intent intentNuevaNota = new Intent(this, typeof(NuevaNotaActivity));
                //StartActivity(intentNuevaNota);
               
            }
            
            else
            { Toast.MakeText(this, "no implementado", ToastLength.Short).Show(); }

            

            return base.OnOptionsItemSelected(item);

           

        }

        //Creamos el PopUp menu para escoger la opcion crear  notas o lista de tareas
        void MenuOptionsNote( )
        {
           
           
               
           
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
            bundleNota.PutString("FechaModificacion", detalleListaNotas.FechaModificacion.ToString());
            intentDetalleNota.PutExtras(bundleNota);
            StartActivity(intentDetalleNota, ActivityOptions.MakeSceneTransitionAnimation(this).ToBundle());

           
           

        }

      
        

    }
   
    
}
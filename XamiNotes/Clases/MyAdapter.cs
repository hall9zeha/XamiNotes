using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
//using Android.Support.Transitions;
using Android.Support.V7.Widget;
using Android.Transitions;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using Java.Lang;
using XamiNotes.Modelo;
using String = System.String;

namespace XamiNotes.Clases
{
    public class MyAdapter : RecyclerView.Adapter,IFilterable
    {
       
        public JavaList<MisNotas> notaspublic;
        private readonly JavaList<MisNotas> listaFiltrada;
        public RecyclerView recyclerNota;
        //Primera sobrecarga del constructor
        public MyAdapter(JavaList<MisNotas> notasPublic, RecyclerView recyclerNota, Activity activity)
        {
            notaspublic = notasPublic;
            listaFiltrada = notasPublic;
            this.recyclerNota = recyclerNota;
            this._context = activity;
        }
        //******************************
        private int resource;
        public readonly Activity _context;
        
        public override int ItemCount
        {
            get { return notaspublic.Size(); }
        }
        //implementamos interfaz de IFilterable
        public Filter Filter
        {
            get { return NotasFilter.nuevoFiltro(this, listaFiltrada); }
        }
        
        public void setNotasFiltradas(JavaList<MisNotas> notasFiltradas)
        {
            notaspublic = notasFiltradas;
        }
        
       
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.CardViewNotalayout, parent, false);
            
            NoteViewHolder nt = new NoteViewHolder(view);
            
            return nt;
            

        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)

        {
          
            NoteViewHolder nota = holder as NoteViewHolder;
            if (nota != null)
            {
               //Usamos este método para construir los elementos a mostrar en cada cardview con el contenido de cada nota  que tengamos
                Metodos.FormatoTextoYFechaContenido(notaspublic[position].Titulo, notaspublic[position].Contenido, notaspublic[position].FechaNota, nota.notaCard, nota.fechaNotas);
                nota.item.Click -= Item_Click;
                nota.item.Click += Item_Click;
                //*************
            }
           //Creamos las notas con colores de fondo para cada nota
            Metodos.CambiarColorNotaHolder(nota.separadorCard, nota.notasCardView, notaspublic[position].IdColor);
            //Cargamos el tipo de letra para cada target o nota
            Metodos.CambiarFont(notaspublic[position].IdFont, _context, nota.notaCard, nota.fechaNotas);
            

        }

        private void Item_Click(object sender, EventArgs e)
        {
            int position = this.recyclerNota.GetChildAdapterPosition((View)sender);
            //MisNotas notasClicked = notaspublic[position];
            //if (notasClicked.Titulo != null)
            //{
            //    Toast.MakeText(this._context, "El Titulo es " + notaspublic[position].Titulo.ToString(), ToastLength.Short).Show();
            //}
            

            Intent intentDetalleNota = new Intent(this._context, typeof(DetalleNotasActivity));
            Bundle bundleNota = new Bundle();
            bundleNota.PutInt("IdNotas", notaspublic[position].IdNotas);
            bundleNota.PutInt("IdColor", notaspublic[position].IdColor);
            bundleNota.PutInt("IdFont", notaspublic[position].IdFont);
            bundleNota.PutString("Titulo", notaspublic[position].Titulo);
            bundleNota.PutString("Contenido", notaspublic[position].Contenido);
            bundleNota.PutString("FechaNota", notaspublic[position].FechaNota.ToString());
            bundleNota.PutString("FechaModificacion", notaspublic[position].FechaModificacion.ToString());
            intentDetalleNota.PutExtras(bundleNota);

            Slide slide = new Slide(GravityFlags.Right);
            slide.SetDuration(600);
            slide.SetInterpolator(new DecelerateInterpolator());
            _context.Window.ExitTransition = slide;
            _context.Window.EnterTransition = slide;
            _context.Window.ReturnTransition = slide;
            this._context.StartActivity(intentDetalleNota,ActivityOptions.MakeSceneTransitionAnimation(_context).ToBundle());


        }

        //***Creamos la clase para filtrar el recycler view
        private class NotasFilter : Filter
        {
            static JavaList<MisNotas> listaActual;
            static  MyAdapter _notasAdapter;
            public static NotasFilter nuevoFiltro(MyAdapter notasAdapter, JavaList<MisNotas> lista)
            {
                _notasAdapter = notasAdapter;
                listaActual = lista;
                return new NotasFilter();
            }
            
            protected override FilterResults PerformFiltering(ICharSequence constraint)
            {
                MisNotas objnota;
                FilterResults filterResults = new FilterResults();
                if (constraint != null && constraint.Length() > 0)
                {
                    string query = constraint.ToString().ToUpper();
                    JavaList<MisNotas> notasEncontradas = new JavaList<MisNotas>();
                    for (int i = 0; i < listaActual.Size(); i++)
                    {
                        objnota = new MisNotas();
                        objnota.Contenido= listaActual[i].Contenido;


                        if (objnota.Contenido.ToUpper().Contains(query.ToString())) 
                        {
                            objnota.Contenido = listaActual[i].Contenido;
                            objnota.Titulo = listaActual[i].Titulo;
                            objnota.FechaNota = listaActual[i].FechaNota;
                            objnota.IdColor = listaActual[i].IdColor;
                            objnota.IdFont = listaActual[i].IdFont;
                            notasEncontradas.Add(objnota);
                            
                        }
                    }
                    filterResults.Count = notasEncontradas.Size();
                    filterResults.Values = notasEncontradas;
                }
                else
                {
                    //NO ITEM FOUND.LIST REMAINS INTACT
                    filterResults.Count = listaActual.Size();
                    filterResults.Values = listaActual;
                }
                return filterResults;
            }

            protected override void PublishResults(ICharSequence constraint, FilterResults results)
            {
                _notasAdapter.setNotasFiltradas((JavaList<MisNotas>)results.Values);
                _notasAdapter.NotifyDataSetChanged();
            }
        }
        //********

    }
    public class NoteViewHolder : RecyclerView.ViewHolder
    {
        public View item { get; set; }
        public TextView notaCard;
        public TextView fechaNotas, fechaModificacion;
        public View separadorCard;
        public CardView notasCardView;

        public NoteViewHolder(View itemView) : base(itemView)
        {
            // Locate and cache view references:
            this.item = itemView;
            notasCardView = itemView.FindViewById<CardView>(Resource.Id.cardNotes);
            notaCard = itemView.FindViewById<TextView>(Resource.Id.textCardView);
            fechaNotas = itemView.FindViewById<TextView>(Resource.Id.textFechaNota);
            fechaModificacion = itemView.FindViewById<TextView>(Resource.Id.fechaUpdateTextView);
            separadorCard = itemView.FindViewById<View>(Resource.Id.separadorViewCard);
           
            notaCard.SetTypeface(Typeface.SansSerif, TypefaceStyle.BoldItalic);
            fechaNotas.SetTypeface(Typeface.SansSerif, TypefaceStyle.BoldItalic);

            
        }

    }
   

}
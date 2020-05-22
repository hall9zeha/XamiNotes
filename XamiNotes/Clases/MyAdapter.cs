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
using XamiNotes.Modelo;

namespace XamiNotes.Clases
{
    public class MyAdapter : RecyclerView.Adapter
    {

        public List<MisNotas> notaspublic;
        public MyAdapter(List<MisNotas> notasPublic)
        {
            notaspublic = notasPublic;
        }
        public event EventHandler<int> ItemClick;
        private int resource;
        private Activity activity;
        private List<MisNotas> lista;
        public override int ItemCount
        {
            get { return notaspublic.Count(); }
        }
        void OnClick(int position)
        {
            if (ItemClick != null)
                ItemClick(this, position);
        }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.CardViewNotalayout, parent, false);
            
            NoteViewHolder nt = new NoteViewHolder(view, OnClick);
            
            return nt;

            
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)

        {
            
           
            NoteViewHolder nota = holder as NoteViewHolder;
            //Formateamos la fecha para mostra los meses en español y el día
            string fechaFormateada = notaspublic[position].FechaNota.ToString("MMMM" + " dd", CultureInfo.CreateSpecificCulture("es-PE"));
            var fecha = fechaFormateada.Substring(0, 1).ToUpper() + fechaFormateada.Substring(1).ToLower();
            //*****
            //Cortamos el contenido si es muy grande y lo mostramos en lugar del título si no existe el mismo
            if (notaspublic[position].Titulo == null || notaspublic[position].Titulo == "")
            {
                int cortar = 0;
                int maxCar = 30;
                string contenidoCortado = "";
                if (notaspublic[position].Contenido.Length > maxCar)
                {
                    cortar = notaspublic[position].Contenido.Length - maxCar;
                    contenidoCortado = notaspublic[position].Contenido.Remove(maxCar, cortar);
                    nota.notaCard.Text = contenidoCortado + "...";
                    nota.fechaNotas.Text = fecha;
                }
                else if (notaspublic[position].Contenido.Length < maxCar)
                {
                    nota.notaCard.Text = notaspublic[position].Contenido;
                    nota.fechaNotas.Text = fecha;
                }
                else
                {
                    nota.notaCard.Text = notaspublic[position].Titulo;
                    nota.fechaNotas.Text = fecha;
                }
            }
            else

            {
                nota.notaCard.Text = notaspublic[position].Titulo;
                nota.fechaNotas.Text = fecha;
            }
            //*************
            //Creamos las notas con colores de fondo para cada nota
            if (notaspublic[position].IdColor == 1)
            {

                nota.notasCardView.SetCardBackgroundColor(Android.Graphics.Color.LightYellow);
                nota.separadorCard.SetBackgroundColor(Android.Graphics.Color.ParseColor("#FFC107"));
            }
            else if (notaspublic[position].IdColor == 2)
            {
                
                nota.notasCardView.SetCardBackgroundColor(Android.Graphics.Color.LightGreen);
                nota.separadorCard.SetBackgroundColor(Android.Graphics.Color.ParseColor("#4CAF50"));
            }
            else if (notaspublic[position].IdColor == 3)
            {
                
                nota.notasCardView.SetCardBackgroundColor(Android.Graphics.Color.LightBlue);
                nota.separadorCard.SetBackgroundColor(Android.Graphics.Color.ParseColor("#03A9F4"));
            }
            else if (notaspublic[position].IdColor == 4)
            {
                
                nota.notasCardView.SetCardBackgroundColor(Android.Graphics.Color.LightSalmon);
                nota.separadorCard.SetBackgroundColor(Android.Graphics.Color.ParseColor("#FF5722"));
            }
            else if (notaspublic[position].IdColor == 5)
            {
                
                nota.notasCardView.SetCardBackgroundColor(Android.Graphics.Color.ParseColor("#CE4BEB"));
                nota.separadorCard.SetBackgroundColor(Android.Graphics.Color.ParseColor("#673AB7"));
            }

            
            

        }

       

       
    }
    public class NoteViewHolder : RecyclerView.ViewHolder
    {
       
        public TextView notaCard;
        public TextView fechaNotas;
        public View separadorCard;
        public CardView notasCardView;

        public NoteViewHolder(View itemView, Action<int> listener) : base(itemView)
        {
            // Locate and cache view references:
            notasCardView = itemView.FindViewById<CardView>(Resource.Id.cardNotes);
            notaCard = itemView.FindViewById<TextView>(Resource.Id.textCardView);
            fechaNotas = itemView.FindViewById<TextView>(Resource.Id.textFechaNota);
            separadorCard = itemView.FindViewById<View>(Resource.Id.separadorViewCard);
            itemView.Click += (sender, e) => listener(base.LayoutPosition);
            notaCard.SetTypeface(Typeface.SansSerif, TypefaceStyle.BoldItalic);
            fechaNotas.SetTypeface(Typeface.SansSerif, TypefaceStyle.BoldItalic);
        }
    }
   
}
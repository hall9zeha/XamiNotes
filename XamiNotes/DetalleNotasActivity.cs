using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;

using Android.Text;
using Android.Text.Style;
using Android.Transitions;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using XamiNotes.Clases;
using XamiNotes.DataBase;
using XamiNotes.Modelo;

namespace XamiNotes
{
    [Activity(Label = "DetalleNotasActivity")]
    public class DetalleNotasActivity : Activity
    {

        Toolbar toolbarDetalle;
        EditText tituloDetalle, detalleNota ;
        TextView fechaNota, fechaModificacion;
        LinearLayout detalleLinear, tituloDetalleLinear, linearTitulo, linearFecha, linearFechaUpdate;
        ColorDrawable colorBackground;
        int idNota, idColor, idFont;
        int color=1;
        string fechaNotaCreacion;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.DetalleNotalayout);
            toolbarDetalle = FindViewById<Toolbar>(Resource.Id.toolbarDetalleNotas);
            tituloDetalle = FindViewById<EditText>(Resource.Id.tituloDetalleEditText);
            detalleNota = FindViewById<EditText>(Resource.Id.detalleNotaEditText);
            fechaNota = FindViewById<TextView>(Resource.Id.fechaTextView);
            fechaModificacion = FindViewById<TextView>(Resource.Id.fechaUpdateTextView);
            toolbarDetalle = FindViewById<Toolbar>(Resource.Id.toolbarDetalleNotas);
            detalleLinear = FindViewById<LinearLayout>(Resource.Id.detalleNotaLinearLayout);
            tituloDetalleLinear = FindViewById<LinearLayout>(Resource.Id.tituloDetalleLinear);
            linearTitulo = FindViewById<LinearLayout>(Resource.Id.linearTitulo);
            linearFecha = FindViewById<LinearLayout>(Resource.Id.linearFecha);
            linearFechaUpdate = FindViewById<LinearLayout>(Resource.Id.linearFechaUpdate);
            SetActionBar(toolbarDetalle);
            ActionBar.Title = "Detalle Nota";
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            CargarDetalleNota();
            //colorear fondo de la nota de acurerdo a el idColor de la tabla

            color = Metodos.CambiarColorNotasGeneric(this, null, idColor,2);
            
            habilitarControles(false, false, false, false);

            //cambiando font del detalle nota
            //cambiarFont();
            
            Window.EnterTransition = transicion();
            Window.ExitTransition = transicion();
            Window.ReturnTransition = transicion();
            //Window.AllowEnterTransitionOverlap = false;
           
        }
        
        private Slide transicion()
        {
            Slide slide = new Slide(GravityFlags.Right);
            slide.SetDuration(400);
            slide.SetInterpolator(new DecelerateInterpolator());
            return slide;
        }
        void habilitarControles(bool focusTitulo,bool focusNota, bool titulo, bool nota)
        {
            
            tituloDetalle.Enabled=titulo;
            detalleNota.Enabled = nota;
            detalleNota.FocusableInTouchMode = focusNota;
            tituloDetalle.FocusableInTouchMode = focusTitulo;

        }
             
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.DetalleNotaMenu,menu);
            //var xamifont = Typeface.CreateFromAsset(Assets, "@font/pacifico.ttf");
            return base.OnCreateOptionsMenu(menu);
            
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
            {
               
                FinishAfterTransition();
                return true;
            }
            if (item.TitleFormatted.ToString() == "Editar")
            {
                EditarNota();
                habilitarControles(false, false, false, false);
            }
            if (item.TitleFormatted.ToString() == "EditarNota")
            {
                habilitarControles(true, true, true, true);
            }
            else if (item.TitleFormatted.ToString() == "Eliminar")
            {
                EliminarNota();
            }


                color = Metodos.CambiarColorNotasGeneric(this, item, idColor,2);
                idFont = Metodos.RegistrarFont(this,item, tituloDetalle, detalleNota, idFont);
                return base.OnOptionsItemSelected(item);
        }
       
        public void CargarDetalleNota()
        {
            tituloDetalle.Text = Intent.Extras.GetString("Titulo");
            detalleNota.Text = Intent.Extras.GetString("Contenido");
            fechaNota.Text = Intent.Extras.GetString("FechaNota");
            //cargamos estas variables para usarlas en diversas funciones
            idNota = Intent.Extras.GetInt("IdNotas");
            idColor = Intent.Extras.GetInt("IdColor");
            idFont = Intent.Extras.GetInt("IdFont");
            color = Intent.Extras.GetInt("IdColor");
            
            fechaNotaCreacion = Intent.Extras.GetString("FechaNota");
            fechaModificacion.Text= Intent.Extras.GetString("FechaModificacion");
            Metodos.CambiarFont(idFont, this, detalleNota, tituloDetalle);

            DateTime fechaNotaRegistrada = Convert.ToDateTime(fechaModificacion.Text);
            DateTime fechaNotaCorta = fechaNotaRegistrada.AddHours(23).AddMinutes(59).AddSeconds(59);
            var fechaRegistro = DateTime.Compare(fechaNotaCorta, DateTime.Now);
            if (fechaRegistro == -1)
            {
                DateTime fechaObj= Convert.ToDateTime(Intent.Extras.GetString("FechaModificacion"));
                fechaModificacion.Text ="Modificado" + fechaObj.ToString(" MMMM" +" dd", CultureInfo.CreateSpecificCulture("es-PE"));

            }
            else
            {
                
                String fechaEnLetras = "Modificado Hoy";
                fechaModificacion.Text = fechaEnLetras;
            }

        }
        public void EditarNota()
        {
            MisNotas objNota = new MisNotas()
            {
                IdNotas=idNota,
                Titulo = tituloDetalle.Text,
                Contenido = detalleNota.Text,
                FechaNota=Convert.ToDateTime(fechaNotaCreacion.ToString()),
                FechaModificacion = DateTime.Now,
                IdColor = color,
                IdFont=idFont
                

                
            };
           int respuesta= MisNotasDb.EditarNota(objNota);
            if (respuesta > 0)
                Toast.MakeText(this, "Modificaciones Guardadas", ToastLength.Long).Show();
            else
                Toast.MakeText(this, "Se produjo un error", ToastLength.Short).Show();
        }
        public void EliminarNota()
        {
            MisNotas objNota = new MisNotas() {
              IdNotas = idNota
            };

            AlertDialog.Builder dialogo = new AlertDialog.Builder(this);
            AlertDialog alert = dialogo.Create();
            alert.SetTitle("Advertencia");
            alert.SetMessage("Realmente quiere eliminar esta nota?");
            alert.SetIcon(Resource.Mipmap.ic_action_warning);
            alert.SetButton("OK", (c, ev) =>
            {
                    int respuesta = MisNotasDb.EliminarNota(objNota);
                    if (respuesta > 0)
                    {   
                            Toast.MakeText(this, "Nota Eliminada", ToastLength.Long).Show();
                            Intent intentBack = new Intent(this, typeof(ListaNotasActivity));
                            StartActivity(intentBack,ActivityOptions.MakeSceneTransitionAnimation(this).ToBundle());
               
               
                    }
                    else
                        Toast.MakeText(this, "Se produjo un error", ToastLength.Short).Show();
                    });
            alert.SetButton2("CANCEL", (c, ev) => {

                Toast.MakeText(this, "Cancelado", ToastLength.Long).Show();
            });
            alert.Show();
        }
       
    }
}
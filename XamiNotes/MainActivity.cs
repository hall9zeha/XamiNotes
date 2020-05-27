using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;
using Android.Transitions;
using Android.Views.Animations;
using Android.Views;



namespace XamiNotes
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            Button boton = FindViewById<Button>(Resource.Id.buttonNuevo);
            boton.Click += Boton_Click;

           //Al salir de la actividad se aplicará esta transición
            Window.EnterTransition = transicion();
            //Al regresar a la transición será esta transición
            Window.ExitTransition = transicion();

        }
        //Crearemos efecto de transición
        private Slide transicion()
        {
          Slide slide = new Slide(GravityFlags.Right);
            slide.SetDuration(500);
            slide.SetInterpolator(new DecelerateInterpolator());
            

            return slide;

        }
        private void Boton_Click(object sender, System.EventArgs e)
        {
            
            Intent intentListaNotas = new Intent(this, typeof(ListaNotasActivity));
            StartActivity(intentListaNotas, ActivityOptions.MakeSceneTransitionAnimation(this).ToBundle());

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
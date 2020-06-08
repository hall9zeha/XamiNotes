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

namespace XamiNotes
{
    [Activity(Label = "ListaDeTareasActivity")]
    public class ListaDeTareasActivity : Activity
    {
        RelativeLayout tareasLayout;
        ViewGroup layoutListaTareas;
        Button agregarTareaBtn;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ListaDeTareasLayout);
            layoutListaTareas = FindViewById<ViewGroup>(Resource.Id.contentLinearLayout);
            agregarTareaBtn = FindViewById<Button>(Resource.Id.nuevaTareaBtn);
            agregarTareaBtn.Click += AgregarTareaBtn_Click;

        }

        private void AgregarTareaBtn_Click(object sender, EventArgs e)
        {
            Toast.MakeText(this, "No implementado ", ToastLength.Short).Show();
            //agregarTarea();
        }

        void agregarTarea()
        {
            int id = Resource.Layout.NuevaNotalayout;
            LayoutInflater inflater = LayoutInflater.From(this);
            
            RelativeLayout tareas = (RelativeLayout)inflater.Inflate(id, null, false);
            this.layoutListaTareas.AddView(tareas);
        }
    }
}
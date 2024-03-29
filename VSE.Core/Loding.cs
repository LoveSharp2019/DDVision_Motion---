using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace VSE.Core
{
    public class Loding
    {
        public static bool IsLoading;
        public static void StartLoading()
        {
            try
            {
                //if (Dog.CheckActive()!=0)
                //{
                //    return;
                //}
                IsLoading = true;
                if (!Loding.isLoading && !Loding.isMainLoading)
                {
                    Loding.isLoading = true;
                    Loding._loading = new Thread(new ThreadStart(Loding.Open));
                    Loding._loading.SetApartmentState(ApartmentState.STA);
                    Loding._loading.IsBackground = true;
                    Loding._loading.Start();
                }

            }
            catch
            {
            }
        }
        public static void StopLoading()
        {

            try
            {
                IsLoading = false;
                if (Loding.isLoading && !Loding.isMainLoading)
                {
                    Loding.isLoading = false;
                    if (Loding._fLoading != null)
                    {
                        Loding._fLoading.Close();
                        Loding._fLoading.Dispose();
                    }
                    Thread.Sleep(200);
                    if (Loding._loading != null)
                    {
                        Loding._loading.Abort();
                    }
                }
            }
            catch
            {
            }

        }
        public static void StartMainLoading()
        {
            try
            {
                IsLoading = true;
                if (!Loding.isMainLoading)
                {
                    Loding.isMainLoading = true;
                    Loding._loading = new Thread(new ThreadStart(Loding.Open));
                    _loading.SetApartmentState(ApartmentState.STA);
                    Loding._loading.IsBackground = true;
                    Loding._loading.Start();
                }
            }
            catch
            {
            }
        }
        public static void SetProcess(int percent, string msg)
        {
            if (Loding._fLoading != null)
            {
                Loding._fLoading.SetProcess(percent, msg);
            }
        }
        public static void StopMainLoading()
        {
            Thread.Sleep(500);
            try
            {
                IsLoading = false;
                if (Loding.isMainLoading)
                {
                    Loding.isMainLoading = false;
                    if (Loding._fLoading != null)
                    {
                        Loding._fLoading.LoadingFinish();
                    }
                    Thread.Sleep(200);
                    if (Loding._loading != null)
                    {
                        Loding._loading.Abort();
                    }
                }
            }
            catch
            {
            }
        }
        public static event EventHandler ExitEvent;

        public static void Exit()
        {
            if (Loding.ExitEvent != null)
            {
                Loding.ExitEvent.BeginInvoke(null, null, null, null);
            }
        }
        public static void Open()
        {
            try
            {
                Loding._fLoading = new frmLoadings(new Point(0, 0), Screen.PrimaryScreen.WorkingArea.Size);
                Loding._fLoading.TopLevel = true;
                Loding._fLoading.ShowDialog();
            }
            catch (Exception)
            {
            }
        }
        private static Thread _loading;
        private static bool isLoading;
        private static bool isMainLoading;
        private static frmLoadings _fLoading;
    }
}

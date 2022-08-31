using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
namespace  IF97
{
    class FreeSteam
    {
        public const string dll_name = "freesteam.dll";
        public struct SteamState_R1
        {
            public double p, T;
        };

        public struct SteamState_R2
        {
            public double p, T;
        };

        public struct SteamState_R3
        {
            public double rho, T;
        };

        public struct SteamState_R4
        {
            public double T, x;
        };

        public struct SteamState
        {
            public short region;
            public SteamState_R1 R1;
            public  SteamState_R2 R2;
            public SteamState_R3 R3;
            public SteamState_R4 R4;
        };
        public FreeSteam()
        {
        }
        //steam.h
        [DllImport(dll_name, EntryPoint = "freesteam_region")]
        public static extern int freesteam_region(SteamState S);

        [DllImport(dll_name, EntryPoint = "freesteam_region1_set_pT")]
        public static extern SteamState freesteam_region1_set_pT(double p, double T);

        [DllImport(dll_name, EntryPoint = "freesteam_region2_set_pT")]
        public static extern SteamState freesteam_region2_set_pT(double p, double T);

        [DllImport(dll_name, EntryPoint = "freesteam_region3_set_rhoT")]
        public static extern SteamState freesteam_region3_set_rhoT(double rho, double T);

        [DllImport(dll_name, EntryPoint = "freesteam_region4_set_Tx")]
        public static extern SteamState freesteam_region4_set_Tx(double T, double x);

        [DllImport(dll_name, EntryPoint = "freesteam_p")]
        public static extern double freesteam_p(SteamState S);

        [DllImport(dll_name, EntryPoint = "freesteam_T")]
        public static extern double freesteam_T(SteamState S);

        [DllImport(dll_name, EntryPoint = "freesteam_rho")]
        public static extern double freesteam_rho(SteamState S);

        [DllImport(dll_name, EntryPoint = "freesteam_v")]
        public static extern double freesteam_v(SteamState S);

        [DllImport(dll_name, EntryPoint = "freesteam_u")]
        public static extern double freesteam_u(SteamState S);

        [DllImport(dll_name, EntryPoint = "freesteam_h")]
        public static extern double freesteam_h(SteamState S);

        [DllImport(dll_name, EntryPoint = "freesteam_s")]
        public static extern double freesteam_s(SteamState S);

        [DllImport(dll_name, EntryPoint = "freesteam_cp")]
        public static extern double freesteam_cp(SteamState S);

        [DllImport(dll_name, EntryPoint = "freesteam_cv")]
        public static extern double freesteam_cv(SteamState S);

        [DllImport(dll_name, EntryPoint = "freesteam_w")]
        public static extern double freesteam_w(SteamState S);

        [DllImport(dll_name, EntryPoint = "freesteam_x")]
        public static extern double freesteam_x(SteamState S);

        [DllImport(dll_name, EntryPoint = "freesteam_mu")]
        public static extern double freesteam_mu(SteamState S);

        [DllImport(dll_name, EntryPoint = "freesteam_k")]
        public static extern double freesteam_k(SteamState S);

        //steam_ph.h
        [DllImport(dll_name, EntryPoint = "freesteam_bounds_ph")]
        public static extern int freesteam_bounds_ph(double p, double h, int verbose);

        [DllImport(dll_name, EntryPoint = "freesteam_region_ph")]
        public static extern int freesteam_region_ph(double p, double h);

        [DllImport(dll_name, EntryPoint = "freesteam_set_ph")]
        public static extern SteamState freesteam_set_ph(double p, double h);
        //steam_ps.h
        [DllImport(dll_name, EntryPoint = "freesteam_bounds_ps")]
        public static extern int freesteam_bounds_ps(double p, double s, int verbose);

        [DllImport(dll_name, EntryPoint = "freesteam_region_ps")]
        public static extern int freesteam_region_ps(double p, double s);

        [DllImport(dll_name, EntryPoint = "freesteam_set_ps")]
        public static extern SteamState freesteam_set_ps(double p, double s);
        //steam_pT.h
        [DllImport(dll_name, EntryPoint = "freesteam_set_pT")]
        public static extern SteamState freesteam_set_pT(double p, double T);
        //steam_pu.h
        [DllImport(dll_name, EntryPoint = "freesteam_region_pu")]
        public static extern int freesteam_region_pu(double p, double u);

        [DllImport(dll_name, EntryPoint = "freesteam_set_pu")]
        public static extern  SteamState freesteam_set_pu(double p, double u);
        //steam_pv.h
        [DllImport(dll_name, EntryPoint = "freesteam_bounds_pv")]
        public static extern int freesteam_bounds_pv(double p, double v, int verbose);

        [DllImport(dll_name, EntryPoint = "freesteam_region_pv")]
        public static extern int freesteam_region_pv(double p, double v);

        [DllImport(dll_name, EntryPoint = "freesteam_set_pv")]
        public static extern SteamState freesteam_set_pv(double p, double v);
        //steam_Ts.h
        [DllImport(dll_name, EntryPoint = "freesteam_bounds_Ts")]
        public static extern int freesteam_bounds_Ts(double T, double s, int verbose);

        [DllImport(dll_name, EntryPoint = "freesteam_region_Ts")]
        public static extern int freesteam_region_Ts(double T, double s);

        [DllImport(dll_name, EntryPoint = "freesteam_set_Ts")]
        public static extern SteamState freesteam_set_Ts(double T, double s);
        //steam_Tx.h
        [DllImport(dll_name, EntryPoint = "freesteam_bounds_Tx")]
        public static extern int freesteam_bounds_Tx(double T, double x, int verbose);

        [DllImport(dll_name, EntryPoint = "freesteam_region_Tx")]
        public static extern int freesteam_region_Tx(double T, double x);

        [DllImport(dll_name, EntryPoint = "freesteam_set_Tx")]
        public static extern SteamState freesteam_set_Tx(double T, double x);
    }
}

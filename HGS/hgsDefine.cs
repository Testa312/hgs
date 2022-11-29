using System;
using System.Collections.Generic;
using System.Text;
using CalcEngine;
using Npgsql;
namespace HGS
{
    public class itemtag
    {
        public int sisid = 0;
        public int id = 0;
        public short fm = 2;
        public pointsrc PointSrc = pointsrc.sis;
    }
    
    //sw开关量。
    public enum alarmlevel
    {
        no, tv, hl, zh, bv, ll, zl, sw, skip, bad, dtw  //no为未设置报警,bad为坏点
    }
    public class varlinktopoint
    {
        public int sub_id { set; get; }//点id
        //public int cellid { set; get; }
        public string varname { set; get; }
    }
    //点来源。
    public enum pointsrc
    {
        sis,
        calc
    }
    public enum PointState
    {
        Good, Timeout, Bad, Error, Force
    }
    //点计算用，用于分清计算点、高报警和低报警计算公式引用点
    public enum cellid
    {
        main, hl, ll, alarmif
    }
    public class AlarmClassInfo
    {
        public AlarmClassInfo(string name, string info)
        {
        }
        public string Name { get; }
        public string info { get; }
    }
    public class point
    {
        private int _id_sis = 0;//sis点id
        public int Id_sis
        {
            set {
                if (_id_sis != value)
                {
                    Data.inst().Update(this);
                    _id_sis = value;
                }
            }
            get { return _id_sis; }
        }
        private string _nd = "";//节点名。
        public string nd
        {
            set {
                if (_nd != value)
                {
                    Data.inst().Update(this);
                    _nd = value;
                }
            }
            get { return _nd; }
        }
        private string _pn = "";//点名
        public string pn
        {
            set {
                if (_pn != value)
                {
                    Data.inst().Update(this);
                    _pn = value;
                }
            }
            get { return _pn; }
        }
        //public int rt = 0;//点类型
        private string _ed = "";//点描述
        public string ed
        {
            set {
                if (_ed != value)
                {
                    Data.inst().Update(this);
                    _ed = value;
                }
            }
            get { return _ed; }
        }
        //可null
        private double? _tv = null;//量程上限
        public double? tv
        {
            set {
                if (_tv != value)
                {
                    Data.inst().Update(this);
                    _tv = value;
                }
            }
            get { return _tv; }
        }
        private double? _bv = null;//量程下限
        public double? bv
        {
            set {
                if (_bv != value)
                {
                    Data.inst().Update(this);
                    _bv = value;
                }
            }
            get { return _bv; }
        }
        //------------------------------------
        private double? _ll = null;//报警低限
        public double? ll
        {
            set {
                if (_ll != value)
                {
                    Data.inst().Update(this);
                    _ll = value;
                }
            }
            get { return _ll; }
        }
        public double? _LL//浮动定值计算用
        {
            set
            {
                _ll = value;
            }
            get { return _ll; }
        }
        private int _Sound = 0;
        public int Sound
        {
            set
            {
                if (_Sound != value)
                {
                    Data.inst().Update(this);
                    _Sound = value;
                }
            }
            get { return _Sound; }
        }
        private string _Orgformula_ll = "";//计算公式
        public string Orgformula_ll
        {
            set {
                if (_Orgformula_ll != value)
                {
                    Data.inst().Update(this);
                    _Orgformula_ll = value;
                }
            }
            get { return _Orgformula_ll; }
        }
        //public string expformula_ll = "";//展开成点的计算公式
        private Expression _Expression_ll = null;//已解析为表达式树,优化计算速度。
        public Expression Expression_ll
        {
            set { _Expression_ll = value; }
            get { return _Expression_ll; }
        }
        //计算子点id
        private List<varlinktopoint> _lsVar_Point_ll = null;//原始参与计算点列表
        public List<varlinktopoint> lsVar_Point_ll
        {
            set { _lsVar_Point_ll = value; }
            get { return _lsVar_Point_ll; }
        }
        //
        //计算子点id用于进行计算点状态计算。
        private List<point> _listSisCalaExpPointID_ll = null;//展开成sis点的参与计算点列表。
        public List<point> listSisCalaExpPointID_ll
        {
            set { _listSisCalaExpPointID_ll = value; }
            get { return _listSisCalaExpPointID_ll; }
        }
        //------------------------------------------
        private double? _hl = null;//报警高限
        public double? hl
        {
            set
            {
                if (_hl != value)
                {
                    Data.inst().Update(this);
                    _hl = value;
                }
            }
            get { return _hl; }
        }
        public double? _HL////浮动定值计算用
        {
            set
            {
                _hl = value;
            }
            get { return _hl; }
        }
        private string _Orgformula_hl = "";//计算公式
        public string orgformula_hl
        {
            set {
                if (_Orgformula_hl != value)
                {
                    Data.inst().Update(this);
                    _Orgformula_hl = value;
                }
            }
            get { return _Orgformula_hl; }
        }
        private Expression _Expression_hl = null;//已解析为表达式树,优化计算速度。
        public Expression Expression_hl
        {
            set { _Expression_hl = value; }
            get { return _Expression_hl; }
        }
        //计算子点id
        private List<varlinktopoint> _lsVartoPoint_hl = null;//变量与点Id的对应列表
        public List<varlinktopoint> lsCalcOrgSubPoint_hl
        {
            set { _lsVartoPoint_hl = value; }
            get { return _lsVartoPoint_hl; }
        }
        //
        //用于进行计算点点状态计算。
        private List<point> _listSisCalcExpPointID_hl = null; //参与公式计算的sis点列表。
        public List<point> listSisCalaExpPointID_hl
        {
            set { _listSisCalcExpPointID_hl = value; }
            get { return _listSisCalcExpPointID_hl; }
        }
        //-----------------------------------------
        private double? _zh = null;//报警高2限
        public double? zh
        {
            set
            {
                if (_zh != value)
                {
                    Data.inst().Update(this);
                    _zh = value;
                }
            }
            get { return _zh; }
        }
        /*
        private double? _max_zh = null;
        public double? max_zh
        {
            set { _max_zh = value;
                Data.inst().Update(this);
            }
            get { return _max_zh; }
        }*/
        private double? _zl = null;//报警低2限
        public double? zl
        {
            set {
                if (_zl != value)
                {
                    Data.inst().Update(this);
                    _zl = value;
                }
            }
            get { return _zl; }
        }
        /*
        private double? _min_zl = null;
        public double? min_zl
        {
            set { _min_zl = value;
            }
            get { return _min_zl; }
        }*/
        //--------------------
        private pointsrc _pointsrc = 0;//点源0:sis,1:计算点
        public pointsrc pointsrc
        {
            set {
                if (_pointsrc != value)
                {
                    Data.inst().Update(this);
                    _pointsrc = value;
                }
            }
            get { return _pointsrc; }
        }
        private int _ownerid = 0;//专业id
        public int OwnerId
        {
            set {
                if (_ownerid != value)
                {
                    Data.inst().Update(this);
                    _ownerid = value;
                }
            }
            get { return _ownerid; }
        }

        private int _id = -1;//点id
        public int id
        {
            set {
                if (_id != value)
                {
                    Data.inst().Update(this);
                    _id = value;
                }
            }
            get { return _id; }
        }
        private string _eu = "";//点单位
        public string eu
        {
            set {
                if (_eu != value)
                {
                    Data.inst().Update(this);
                    _eu = value;
                }
            }
            get { return _eu; }
        }
        private bool _isCalc = false;//是否进行计算
        public bool isCalc
        {
            set
            {
                if (_isCalc != value)
                {
                    Data.inst().Update(this);
                    _isCalc = value;
                }
            }
            get { return _isCalc; }
        }
        public int AlarmCount = 0;

        private bool _isAvalarm = false;//是否报警
        public bool isAvalarm
        {
            set
            {
                if (_isAvalarm != value)
                {
                    Data.inst().Update(this);
                    _isAvalarm = value;
                }
            }
            get { return _isAvalarm; }
        }
        private PointState _ps = PointState.Good;//点状态,实时计算用
        public PointState ps
        {
            set { _ps = value; }
            get { return _ps; }
        }

        private short _fm = 1;//保留小数点位数。
        public short fm
        {
            set {
                if (_fm != value)
                {
                    Data.inst().Update(this);
                    _fm = value;
                }
            }
            get { return _fm; }
        }

        //----------------------
        private bool _boolAlarmif = true;
        public bool boolAlarmif
        {
            set {
                if (_boolAlarmif != value)
                {
                    Data.inst().Update(this);
                    _boolAlarmif = value;
                }
            }
            get { return _boolAlarmif; }
        }
        private bool _isboolvAlarm = false;
        public bool isboolvAlarm
        {
            set
            {
                if (_isboolvAlarm != value)
                {
                    Data.inst().Update(this);
                    _isboolvAlarm = value;
                }
            }
            get { return _isboolvAlarm; }
        }
        private string _boolAlarminfo = "";//isbool 为真时的报警信息。
        public string boolAlarminfo
        {
            set {
                if (_boolAlarminfo != value)
                {
                    Data.inst().Update(this);
                    _boolAlarminfo = value;
                }
            }
            get { return _boolAlarminfo; }
        }
        //
        private string _Orgformula_main = "";//原始计算公式
        public string Orgformula_main
        {
            set {
                if (_Orgformula_main != value)
                {
                    Data.inst().Update(this);
                    _Orgformula_main = value;
                }
            }
            get { return _Orgformula_main; }
        }
        private string _Sisformula_main = "";//已展开成SIS点的计算公式，不保存
        public string Sisformula_main
        {
            set { _Sisformula_main = value; }
            get { return _Sisformula_main; }
        }
        private Expression _Expression_main = null;//已解析为表达式树,优化计算速度。
        public Expression Expression_main
        {
            set { _Expression_main = value; }
            get { return _Expression_main; }
        }
        //计算子点id
        private List<varlinktopoint> _lsVartoPoint_main = null;//变量与点Id的对应列表
        public List<varlinktopoint> lsCalcOrgSubPoint_main
        {
            set { _lsVartoPoint_main = value; }
            get { return _lsVartoPoint_main; }
        }
        //
        //计算子点id用于进行计算点状态计算。
        private List<point> _listSisCalcExpPointID_main = null;//参与公式计算的sis点列表。;
        public List<point> listSisCalcExpPointID_main
        {
            set { _listSisCalcExpPointID_main = value; }
            get { return _listSisCalcExpPointID_main; }
        }
        //-------------
        //bit0:ZL
        //bit1:LL
        //bit2:HL
        //bit3:ZH
        //bit4:TV
        //bit5:BV
        //bit6:Bool
        //biti7:skip
        //bit8:wave
        //bit9:Bad
        //----------------
        private uint _lastAlarmBitInfo = 0;
        //private double? _Alarmingav = null;

        //强制值，不存数据库
        private bool _isForce = false;
        public bool isForce
        {
            set { _isForce = value; }
            get { return _isForce; }
        }
        private float? _Forceav = null;//强制的数值。
        public float? Forceav
        {
            set { _Forceav = value; }
            get { return _Forceav; }
        }
        ////---------------------报警公式，值为真时才允许报警。
        
        private bool _Alarmifav = true;
        public bool Alarmifav
        {
            set { _Alarmifav = value; }
            get { return _Alarmifav; }
        }
        
        private string _Alarmif = "";
        public string Alarmif
        {
            set {
                if (_Alarmif != value)
                {
                    Data.inst().Update(this);
                    _Alarmif = value;
                }
            }
            get { return _Alarmif; }
        }
        private Expression _Expression_alarmif = null;//已解析为表达式树,优化计算速度。
        public Expression Expression_alarmif
        {
            set { _Expression_alarmif = value; }
            get { return _Expression_alarmif; }
        }
        //计算子点id
        private List<varlinktopoint> _lsVartoPoint_alarmif = null; //变量与点Id的对应列表
        public List<varlinktopoint> lsCalcOrgSubPoint_alarmif
        {
            set { _lsVartoPoint_alarmif = value; }
            get { return _lsVartoPoint_alarmif; }
        }
        //
        //用于进行计算点点状态计算。
        private List<point> _listSisCalcExpPointID_alarmif = null;// 参与公式计算的sis点列表。
        public List<point> listSisCalcExpPointID_alarmif
        {
            set { _listSisCalcExpPointID_alarmif = value; }
            get { return _listSisCalcExpPointID_alarmif; }
        }
        private double? _Skip_pp = null;
        public double? Skip_pp
        {
            set
            {
                if (_Skip_pp != value)
                {
                    Data.inst().Update(this);
                    _Skip_pp = value;
                    if (_Skip_pp != null && DetectionSkip == null)
                    {
                        DetectionSkip = new DetectionSkip();
                    }
                    else if (_Skip_pp == null)
                    {
                        DetectionSkip = null;
                    }
                }
            }
            get { return _Skip_pp; }
        }
        private DetectionSkip _DetectionSkip = null;
        public DetectionSkip DetectionSkip
        {
            set { _DetectionSkip = value; }
            get { return _DetectionSkip; }
        }
        private int _TimeTick = -1;

        private float? _av = null;//点值，实时或计算。
        //-----------------------------
        //动态时间规整器扫描的阈值,6个数，为15m,30m,60m,120m,240m,480m时间段。
        private float[] _dtw_start_th = null;
        //传感器的设备归属-不存数据库，运行时生成。
        private HashSet<int> _hs_Device = null;
        private Dictionary<int,string> _hsDevicePath = null;
        public string DevicePath
        {
            get {
                string path = "";
                if (_hsDevicePath != null)
                    path = _hsDevicePath.Values.ToString();
                return path; 
            }
        }
        private Dtw_queues[] _dtw_Queues_Array = null;
        private float[] _dtw_start_max = new float[6];
        public float[] dtw_start_max
        {
            set { _dtw_start_max = value; }
            get { return _dtw_start_max; }
        }
        private DetectorWave[] _wd3s_Queues_Array = null;
        private float[] _wd3s_th = null;//30s,60s,120s,240s,480s,960s 阈值。
        public point(int id, pointsrc ps)
        {
            _id = id;
            _pointsrc = ps;
            if (id >= 0)//0和负值为临时测试点
                Data.inst().AddNew(this);
        }

        public point(NpgsqlDataReader pgreader)
        {
            if (pgreader == null) return;

            _id = (int)pgreader["id"];
            _nd = pgreader["nd"].ToString();
            _pn = pgreader["pn"].ToString();
            _eu = pgreader["eu"].ToString();
            _ed = pgreader["ed"].ToString();

            _tv = Functions.CasttoDouble(pgreader["tv"]);
            _bv = Functions.CasttoDouble(pgreader["bv"]);
            _ll = Functions.CasttoDouble(pgreader["ll"]);
            _hl = Functions.CasttoDouble(pgreader["hl"]);
            _zl = Functions.CasttoDouble(pgreader["zl"]);
            _zh = Functions.CasttoDouble(pgreader["zh"]);
            _Skip_pp = Functions.CasttoDouble(pgreader["skip_pp"]);

            _id_sis = (int)pgreader["id_sis"];

            _pointsrc = (pointsrc)(short)pgreader["pointsrc"];
            _ownerid = (int)pgreader["ownerid"];
            _Orgformula_main = pgreader["orgformula_main"].ToString();

            _isAvalarm = (bool)pgreader["isavalarm"];
            _isCalc = (bool)pgreader["iscalc"];
            _fm = (short)pgreader["fm"];
            _isboolvAlarm = (bool)pgreader["isboolv"];
            _boolAlarminfo = pgreader["boolalarminfo"].ToString();
            _Orgformula_hl = pgreader["orgformula_hl"].ToString();
            _Orgformula_ll = pgreader["orgformula_ll"].ToString();
            _Alarmif = pgreader["alarmif"].ToString();
            _boolAlarmif = (bool)pgreader["boolalarmif"];

            //_isAlarmskip = (bool)pgreader["isalarmskip"];
            //_isAlarmwave = (bool)pgreader["isalarmwave"];
            _Sound = (int)pgreader["sound"];
            if (Skip_pp != null)
                _DetectionSkip = new DetectionSkip();
            //
            object ob = pgreader["dtw_start_th"];
            if (ob != DBNull.Value)
            {
                _dtw_start_th = (float[])ob;
            }
            ob = pgreader["wave_th"];
            if (ob != DBNull.Value)
            {
                _wd3s_th = (float[])ob;
            }
            Data.inst().Add(this);
        }
        //-----------------------------
        //不能多于一次赋值，否则将不对。
        public float? av
        {
            get { return _av; }
            set
            {
                _av = value;
                //_datanums++;
                if (_DetectionSkip != null)
                {
                    _DetectionSkip.add(_av ?? 0);
                }
                if (_dtw_Queues_Array != null)
                {
                    for (int i = 0; i < _dtw_Queues_Array.Length; i++)
                    {
                        _dtw_Queues_Array[i].add(_av ?? 0, true);
                    }
                }
                //
                if (_wd3s_Queues_Array != null)
                {
                    for (int i = 0; i < _wd3s_Queues_Array.Length; i++)
                    {
                        _wd3s_Queues_Array[i].add(_av ?? 0, true);
                    }
                }
            }
        }
        public float[] Dtw_start_th
        {
            get { return _dtw_start_th; }
            set
            {
                if (_dtw_start_th != value)
                {
                    Data.inst().Update(this);
                    _dtw_start_th = value;
                    if (_dtw_start_th != null)
                    {
                        if (_dtw_start_th.Length != 6)
                            throw new Exception("dtw阈值数必须为6个!");
                    }
                    initDeviceQ();
                }
            }
        }      
        public Dtw_queues[] Dtw_Queues_Array
        {
            get { return _dtw_Queues_Array; }
        }
        public void initDeviceQ(int step, float[] v)
        {
            if (step < 0 || step >= 6)
                throw new Exception("设备没有采集这些数据！");
            if (v == null)
                throw new Exception("数据不能为空！");
            if (_dtw_Queues_Array == null)
            {
                _dtw_Queues_Array = new Dtw_queues[6];
                for (int i = 0; i < _dtw_Queues_Array.Length; i++)
                {
                    _dtw_Queues_Array[i] = new Dtw_queues();
                    _dtw_Queues_Array[i].DownSamples = (int)(9 * Math.Pow(2, i));
                }
            }
            for (int i = 0; i < v.Length; i++)
            {
                _dtw_Queues_Array[step].add(v[i], false);
            }
        }
        //初始化dtw队列数组
        private void initDeviceQ()
        {
            if (_hs_Device != null && _dtw_start_th != null)
            {
                if (_dtw_Queues_Array == null)
                {
                    _dtw_Queues_Array = new Dtw_queues[6];
                    for (int i = 0; i < _dtw_Queues_Array.Length; i++)
                    {
                        _dtw_Queues_Array[i] = new Dtw_queues();
                        _dtw_Queues_Array[i].DownSamples = (int)(9 * Math.Pow(2, i));
                    }
                    //
                    Dictionary<int, point> dic_intQueues = new Dictionary<int, point>();
                    dic_intQueues.Add(id, Data.inst().cd_Point[id]);
                    SisConnect.InitPointDtwQueues(dic_intQueues);
                }
            }
            else
                _dtw_Queues_Array = null;
        }
        //--------------------
        public DetectorWave[] Wd3s_Queues_Array
        {
            get { return _wd3s_Queues_Array; }
        }
        public float[] Wd3s_th
        {
            get { return _wd3s_th; }
            set
            {
                if (_wd3s_th != value)
                {
                    Data.inst().Update(this);
                    _wd3s_th = value;
                    if (_wd3s_th != null)
                    {
                        if (_wd3s_th.Length != 6)
                            throw new Exception("dtw阈值数必须为6个!");
                    }
                    initWave3sQ();
                }
            }
        }
        public void initWave3sQ(int step, float[] v)
        {
            if (step < 0 || step >= 6)
                throw new Exception("设备没有采集这些数据！");
            if (v == null)
                throw new Exception("数据不能为空！");
            if (_wd3s_Queues_Array == null)
            {
                _wd3s_Queues_Array = new DetectorWave[6];
                for (int i = 0; i < _wd3s_Queues_Array.Length; i++)
                {
                    _wd3s_Queues_Array[i] = new DetectorWave((int)Math.Pow(2,i));
                }
            }
            for (int i = 0; i < v.Length; i++)
            {
                _wd3s_Queues_Array[step].add(v[i], false);
            }
        }
        //初始化dtw队列数组
        private void initWave3sQ()
        {
            if (_wd3s_th != null)
            {
                if (_wd3s_Queues_Array == null)
                {
                    _wd3s_Queues_Array = new DetectorWave[6];
                    for (int i = 0; i < _wd3s_Queues_Array.Length; i++)
                    {
                        _wd3s_Queues_Array[i] = new DetectorWave((int)Math.Pow(2, i));
                    }
                    //
                    Dictionary<int, point> dic_intQueues = new Dictionary<int, point>();
                    dic_intQueues.Add(id, Data.inst().cd_Point[id]);
                    SisConnect.InitSensorsWaveQueues(dic_intQueues);
                }
            }
            else
                _wd3s_Queues_Array = null;
        }
        //----------------
        public void addtodevice(int di,string path)
        {
            if (_hs_Device == null)
                _hs_Device = new HashSet<int>();
            _hs_Device.Add(di);
            if (_hsDevicePath == null)
                _hsDevicePath = new Dictionary<int, string>();
            _hsDevicePath[di] = path;
        }
        public void removefromdevice(int di)
        {
            if (_hs_Device != null)
            {
                _hs_Device.Remove(di);
            }
            if (_hsDevicePath != null)
            {
                _hsDevicePath.Remove(di);
            }
            if (_hs_Device.Count == 0)
            {
                _hs_Device = null;
                _hsDevicePath = null;
                _dtw_Queues_Array = null;
                Dtw_start_th = null;
                Data.inst().SavetoPG();
            }
        }
        public HashSet<int> Device_set()
        {
            HashSet<int> hs = new HashSet<int>();
            if (_hs_Device != null)
                hs.UnionWith(_hs_Device);
            return hs;
        }
        private string CreateAlarmSid(int bitnum)
        {
            return string.Format("P{0}-{1}", id, _Alarm[bitnum,0]);
        }
        //-------------
        public AlarmInfo CreateAlarmInfo(int bitnum,double? AlarmAv)
        {
            StringBuilder sb = new StringBuilder();
            if (_hsDevicePath != null)
                foreach (string sp in _hsDevicePath.Values)
                {
                    sb.AppendLine(sp);
                }
            return new AlarmInfo(CreateAlarmSid(bitnum), _id, -1, _nd, _pn, _ed, _eu,
                (float)Functions.NullDoubleRount(AlarmAv, _fm),
                _Alarm[bitnum, 1],
                sb.ToString(),
                _Sound) ;
        }
        //--------------------
        static string[,] _Alarm = {
            {"ZL",  "越低2限报警！"     },//0
            {"LL",  "越低限报警！"      },//1
            {"HL",  "越高限报警！"      },//2
            {"ZH",  "越高2限报警！"     },//3
            {"TV",  "越量程上限报警！"  },//4
            {"BV",  "越量程下限报警！"  },//5
            {"Bool" ,""                 },//6
            {"Skip", "跳变报警！"       },//7
            {"NULL", "NULL"             },//8
            {"Bad",  "坏点"             },//9

            {"Wave90s", "90秒内波动报警"       },//10
            {"Wave180s", "3分钟内波动报警"     },//11
            {"Wave360s", "6分钟内波动报警"     },//12
            {"Wave720s", "12分钟内波动报警"    },//13
            {"Wave1440s", "24分钟内波动报警"   },//14
            {"Wave2880s", "48分钟内波动报警"   }};//15
        //---------------------
        private void SetAlarmBit_H(ref uint alarmbit,int bitnum,double? th)
        {
            const double RATION = 0.98f;
            if (th != null)
            {
                if (_av > th)
                {
                    alarmbit |= (uint)1 << bitnum;
                }
                else if (_av < th * RATION)
                {
                    alarmbit &= ~(uint)1 << bitnum;
                }
            }
            else
                alarmbit &= ~(uint)1 << bitnum;
        }
        private void SetAlarmBit_L(ref uint alarmbit, int bitnum, double? th)
        {
            const double RATION = 1.02f;
            if (th != null)
            {
                if (_av < th)
                {
                    alarmbit |= (uint)1<< bitnum;
                }
                else if (_av > th * RATION)
                {
                    alarmbit &= ~(uint)1<< bitnum;
                }
            }
            else
                alarmbit &= ~(uint)1 << bitnum;
        }
        //素数11, 23, 43, 83, 167, 317
        static int[] prime = { 11, 23, 43, 83, 167, 317 };
        public void AlarmCalc()
        {
            _TimeTick++;
            uint curAlarmBit = 0;
            if (_Alarmifav && _isAvalarm)
            {
                curAlarmBit = (uint)1 << 7;
                curAlarmBit |= (uint)63 << 10;
                curAlarmBit &= _lastAlarmBitInfo;//保留波动和跳变检查
            }           
            if (ps != PointState.Good && ps != PointState.Force)
            {
                //_Alarmingav = -1;
                curAlarmBit |= (uint)1 << 9;

                if (_DetectionSkip != null)
                    _DetectionSkip.Clear();
                if (_dtw_Queues_Array != null)
                {
                    for (int i = 0; i < _dtw_Queues_Array.Length; i++)
                    {
                        _dtw_Queues_Array[i].Clear();
                    }
                }
                //
                if (_wd3s_Queues_Array != null)
                {
                    for (int i = 0; i < _wd3s_Queues_Array.Length; i++)
                    {
                        _wd3s_Queues_Array[i].Clear();
                    }
                }
            }
            else if (_Alarmifav)
            {
                if (_isboolvAlarm)
                {
                    bool blv = Convert.ToBoolean(_av);

                    if (blv == _boolAlarmif)
                    {
                        //_Alarmingav = Convert.ToDouble(blv);
                        curAlarmBit |= (uint)1 << 6;
                    }
                }
                else if (_isAvalarm)
                {
                    SetAlarmBit_H(ref curAlarmBit, 3, _zh);
                    SetAlarmBit_H(ref curAlarmBit, 2, _hl);
                    SetAlarmBit_H(ref curAlarmBit, 4, _tv);

                    SetAlarmBit_L(ref curAlarmBit, 0, _zl);
                    SetAlarmBit_L(ref curAlarmBit, 1, _ll);
                    SetAlarmBit_L(ref curAlarmBit, 5, _bv);
                   
                    if ((_id + _TimeTick) % 7 == 0)
                    {
                        curAlarmBit &= ~((uint)1 << 7);
                        double pp = 0;
                        if (_Skip_pp != null && _DetectionSkip != null)
                        {
                            pp = _DetectionSkip.DeltaP_P();
                            if (pp > _Skip_pp)
                            {
                                curAlarmBit |= (uint)1 << 7;
                            }
                        }
                        //
                        uint lastBit = _lastAlarmBitInfo & ((uint)1 << 7);
                        uint curBit = curAlarmBit & ((uint)1 << 7);
                        if (lastBit > curBit)
                        {
                            AlarmSet.GetInst().AlarmStop(CreateAlarmSid(7));
                        }
                        else if (lastBit < curBit)
                        {
                            AlarmInfo ai = CreateAlarmInfo(7, pp);
                            AlarmSet.GetInst().AddAlarming(ai);
                            AlarmCount++;
                        }
                        _lastAlarmBitInfo &= ~((uint)1 << 7);
                        _lastAlarmBitInfo |= curBit;
                    }
                    if (Wd3s_Queues_Array != null && Wd3s_th != null)
                    {
                        for (int i = 0; i < prime.Length; i++)
                        {
                            if ((_id + _TimeTick) % prime[i] == 0)
                            {
                                curAlarmBit &= ~((uint)1 << (i + 10)); ;
                                if (Wd3s_Queues_Array[i].IsWave(Wd3s_th[i]))
                                    curAlarmBit |= ((uint)1 << (i + 10));
                                //
                                uint lastBit = _lastAlarmBitInfo & ((uint)1 << i + 10);
                                uint curBit = curAlarmBit & ((uint)1 << i + 10);
                                if (lastBit > curBit)
                                {
                                    AlarmSet.GetInst().AlarmStop(CreateAlarmSid(i + 10));
                                }
                                else if (lastBit < curBit)
                                {
                                    AlarmInfo ai = CreateAlarmInfo(i + 10, Wd3s_Queues_Array[i].Delta_pp());
                                    AlarmSet.GetInst().AddAlarming(ai);
                                    AlarmCount++;
                                }
                                _lastAlarmBitInfo &= ~((uint)1 << i + 10);
                                _lastAlarmBitInfo |= curBit;
                            }
                        }
                    }
                }
                    //--------------
            }
            //
            for (int a = 0; a <= 15; a++)
            {
                uint lastBit = _lastAlarmBitInfo & ((uint)1 << a);
                uint curBit = curAlarmBit & ((uint)1 << a);
                if (lastBit > curBit)
                {
                    AlarmSet.GetInst().AlarmStop(CreateAlarmSid(a));
                }
                else if (lastBit < curBit)
                {
                    AlarmInfo ai = CreateAlarmInfo(a,_av);
                    if (a == 6)
                        ai._Info = boolAlarminfo;
                    AlarmSet.GetInst().AddAlarming(ai);
                    AlarmCount++;
                }
            }
            _lastAlarmBitInfo = curAlarmBit;
        }
    }
   
}

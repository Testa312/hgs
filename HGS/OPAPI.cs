using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections;

using op_table = System.IntPtr;
using op_request = System.IntPtr;
using op_response = System.IntPtr;
using op_stream = System.IntPtr;
using op_row = System.IntPtr;
using OPAsyncHandler = System.IntPtr;
using OpenPlant = System.IntPtr;


namespace OPAPI
{
    public class eType
    {
        public const int Null = 0;
        public const int Bool = 1;
        public const int Int8 = 2;
        public const int Int16 = 3;
        public const int Int32 = 4;
        public const int Int64 = 5;
        public const int Float = 6;
        public const int Double = 7;
        public const int DateTime = 8;
        public const int String = 9;
        public const int Binary = 10;
        public const int Object = 11;
    }

    public class IO
    {
        public IO()
        {
        }
        public const string dll_name = "opapi4.dll";

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void event_cb(IntPtr owner, op_response response);

        [DllImport(dll_name, EntryPoint = "op2_init")]
        public static extern IntPtr init(int option
                , string host, int port, int timeout
                , string user, string password
                , string buffer_path, int buffer_size);

        [DllImport(dll_name, EntryPoint = "op2_close")]
        public static extern void close(IntPtr op);

        [DllImport(dll_name, EntryPoint = "op2_status")]
        public static extern int status(IntPtr op);

        [DllImport(dll_name, EntryPoint = "op2_try_connect")]
        public static extern int try_connect(OpenPlant op);

        [DllImport(dll_name, EntryPoint = "op2_get_system_time")]
        public static extern int get_system_time(IntPtr op, ref int time);

        [DllImport(dll_name, EntryPoint = "op2_decode_time")]
        public static extern int decode_time(int time, ref int yy, ref int mm, ref int dd, ref int hh, ref int mi, ref int ss);

        [DllImport(dll_name, EntryPoint = "op2_new_group")]
        public static extern IntPtr new_group();

        [DllImport(dll_name, EntryPoint = "op2_free_group")]
        public static extern void free_group(IntPtr gh);

        [DllImport(dll_name, EntryPoint = "op2_add_group_point")]
        public static extern int add_group_point(IntPtr gh, string obj_name);

        [DllImport(dll_name, EntryPoint = "op2_get_value_byname")]
        public static extern int get_value_byname(IntPtr op, IntPtr gh, int[] time, short[] status, double[] value, int[] errors);

        [DllImport(dll_name, EntryPoint = "op2_get_value_byid")]
        public static extern int get_value_byid(IntPtr op, int num, int[] id, int[] time, short[] status, double[] value, int[] errors);

        [DllImport(dll_name, EntryPoint = "op2_get_snap_byid")]
        public static extern int get_snap_byid(IntPtr op, int num, int[] id, int time, short[] status, double[] value, int[] errors);

        [DllImport(dll_name, EntryPoint = "op2_write_value")]
        public static extern int write_value(IntPtr op, int pt, int num, int[] id, int time, short[] status, double[] value, int[] errors);

        [DllImport(dll_name, EntryPoint = "op2_write_snap")]
        public static extern int write_snap(IntPtr op, int pt, int num, int[] id, int time, short[] status, double[] value, int[] errors);

        [DllImport(dll_name, EntryPoint = "op2_new_table")]
        public static extern op_table new_table(string name);

        [DllImport(dll_name, EntryPoint = "op2_new_row")]
        public static extern op_row new_row(op_table table);

        [DllImport(dll_name, EntryPoint = "op2_free_table")]
        public static extern void free_table(op_table table);

        [DllImport(dll_name, EntryPoint = "op2_add_column")]
        public static extern int add_column(op_table table, string name, int type, int length, int mask, string defval, string expr);

        [DllImport(dll_name, EntryPoint = "op2_column_count")]
        public static extern int column_count(op_table table);

        [DllImport(dll_name, EntryPoint = "op2_column_type")]
        public static extern int column_type(op_table table, int col);

        [DllImport(dll_name, EntryPoint = "op2_column_name")]
        public static extern IntPtr column_name_(op_table table, int col);
        public static string column_name(op_table table, int col)
        {
            IntPtr p = column_name_(table, col);
            return Marshal.PtrToStringAnsi(p);
        }

        [DllImport(dll_name, EntryPoint = "op2_column_index")]
        public static extern int column_index(op_table table, string name);

        [DllImport(dll_name, EntryPoint = "op2_append_row")]
        public static extern int append_row(op_table table);

        [DllImport(dll_name, EntryPoint = "op2_new_request")]
        public static extern op_request new_request();

        [DllImport(dll_name, EntryPoint = "op2_free_request")]
        public static extern void free_request(op_request r);

        [DllImport(dll_name, EntryPoint = "op2_get_stream")]
        public static extern op_stream get_stream(IntPtr openplant);

        [DllImport(dll_name, EntryPoint = "op2_set_compress")]
        public static extern void set_compress(op_stream opio, int zip);

        [DllImport(dll_name, EntryPoint = "op2_write_request")]
        public static extern int write_request(op_stream opio, op_request r);

        [DllImport(dll_name, EntryPoint = "op2_write_content")]
        public static extern int write_content(op_stream opio, op_table t);

        [DllImport(dll_name, EntryPoint = "op2_flush_content")]
        public static extern int flush_content(op_stream opio);

        [DllImport(dll_name, EntryPoint = "op2_get_response")]
        public static extern int get_response(op_stream opio, out op_response r);

        [DllImport(dll_name, EntryPoint = "op2_next_content")]
        public static extern int next_content(op_stream opio, op_table result, int clear, out int eof);

        [DllImport(dll_name, EntryPoint = "op2_get_error")]
        public static extern IntPtr get_error_(IntPtr r);
        public static string get_error(IntPtr r)
        {
            return Marshal.PtrToStringAnsi(get_error_(r));
        }

        [DllImport(dll_name, EntryPoint = "op2_free_response")]
        public static extern void free_response(op_response r);

        [DllImport(dll_name, EntryPoint = "op2_set_rowid")]
        public static extern int set_rowid(op_table table, int rowid);

        [DllImport(dll_name, EntryPoint = "op2_append")]
        public static extern int append(op_table table, op_row row);

        [DllImport(dll_name, EntryPoint = "op2_row_count")]
        public static extern int row_count(op_table table);

        [DllImport(dll_name, EntryPoint = "op2_column_int")]
        public static extern Int64 column_int(op_row row, int col);

        [DllImport(dll_name, EntryPoint = "op2_column_double")]
        public static extern double column_double(op_row row, int col);

        [DllImport(dll_name, EntryPoint = "op2_column_string")]
        public static extern IntPtr column_string_(op_row row, int col);
        public static string column_string(op_row row, int col)
        {
            int l = column_bytes(row, col);
            IntPtr p = column_string_(row, col);
            byte[] raw = new byte[l];
            Marshal.Copy(p, raw, 0, l);
            return Encoding.UTF8.GetString(raw);
        }

        [DllImport(dll_name, EntryPoint = "op2_column_binary")]
        public static extern IntPtr column_binary_(op_row row, int col);
        public static byte[] column_binary(op_row row, int col)
        {
            IntPtr p = column_binary_(row, col);
            if (p == IntPtr.Zero)
            {
                return null;
            }
            int size = column_bytes(row, col);
            if (column_type(row, col) == eType.Object)
            {
                size -= 1;
            }
            byte[] bytes = new byte[size];
            Marshal.Copy(p, bytes, 0, size);
            return bytes;
        }

        [DllImport(dll_name, EntryPoint = "op2_column_bytes")]
        public static extern int column_bytes(op_row row, int col);

        [DllImport(dll_name, EntryPoint = "op2_value_type")]
        public static extern int value_type(op_row row, int col);

        [DllImport(dll_name, EntryPoint = "op2_bind_bool")]
        public static extern void bind_bool(op_row row, int col, int value);

        [DllImport(dll_name, EntryPoint = "op2_bind_int8")]
        public static extern void bind_int8(op_row row, int col, int value, Int64 mask = -1);

        [DllImport(dll_name, EntryPoint = "op2_bind_int16")]
        public static extern void bind_int16(op_row row, int col, int value, Int64 mask = -1);

        [DllImport(dll_name, EntryPoint = "op2_bind_int32")]
        public static extern void bind_int32(op_row row, int col, int value, Int64 mask = -1);

        [DllImport(dll_name, EntryPoint = "op2_bind_int")]
        public static extern void bind_int(op_row row, int col, Int64 value, Int64 mask = -1);

        [DllImport(dll_name, EntryPoint = "op2_bind_float")]
        public static extern void bind_float(op_row row, int col, float value);

        [DllImport(dll_name, EntryPoint = "op2_bind_double")]
        public static extern void bind_double(op_row row, int col, double value);

        [DllImport(dll_name, EntryPoint = "op2_bind_string")]
        public static extern void bind_string(op_row row, int col, string value);

        [DllImport(dll_name, EntryPoint = "op2_bind_binary")]
        public static extern void bind_binary(op_row row, int col, byte[] value, int len);

        [DllImport(dll_name, EntryPoint = "op2_set_table")]
        public static extern void set_table(IntPtr r, op_table t);

        [DllImport(dll_name, EntryPoint = "op2_get_table")]
        public static extern op_table get_table(IntPtr r);

        [DllImport(dll_name, EntryPoint = "op2_set_option")]
        public static extern void set_option(IntPtr r, string key, string value);

        [DllImport(dll_name, EntryPoint = "op2_get_option")]
        public static extern string get_option(IntPtr r, string key, string buffer, int len);

        [DllImport(dll_name, EntryPoint = "op2_set_indices")]
        public static extern void set_indices(op_request r, string name, int count, Int64[] keys);

        [DllImport(dll_name, EntryPoint = "op2_set_indices_string")]
        public static extern void set_indices_string(op_request r, string name, int count, string[,] keys);

        [DllImport(dll_name, EntryPoint = "op2_add_filter")]
        public static extern void add_filter(op_request req, string l, int op, string r, int rel);

        [DllImport(dll_name, EntryPoint = "op2_get_errno")]
        public static extern int get_errno(IntPtr r);

        [DllImport(dll_name, EntryPoint = "op2_open_async")]
        public static extern OPAsyncHandler open_async(OpenPlant op, op_request r, event_cb cb, IntPtr owner, out int error);

        [DllImport(dll_name, EntryPoint = "op2_close_async")]
        public static extern void close_async(OPAsyncHandler ah);

        [DllImport(dll_name, EntryPoint = "op2_async_subscribe")]
        public static extern int async_subscribe(OPAsyncHandler ah, int count, Int64[] ids, int onoff);

        [DllImport(dll_name, EntryPoint = "op2_async_subscribe_tags")]
        public static extern int async_subscribe_tags(OPAsyncHandler ah, int count, string[] tags, int onoff);


        // actions
        public static void action_bool(op_row row, int i, object v)
        {
            bool b = (bool)v;
            bind_int8(row, i, (b ? 1 : 0));
        }

        public static void action_byte(op_row row, int i, object v)
        {
            bind_int8(row, i, (byte)v);
        }

        public static void action_sbyte(op_row row, int i, object v)
        {
            bind_int8(row, i, (int)v);
        }

        public static void action_short(op_row row, int i, object v)
        {
            bind_int16(row, i, (short)v);
        }

        public static void action_ushort(op_row row, int i, object v)
        {
            bind_int16(row, i, (ushort)v);
        }

        public static void action_int(op_row row, int i, object v)
        {
            bind_int32(row, i, (int)v);
        }

        public static void action_uint(op_row row, int i, object v)
        {
            bind_int32(row, i, (int)(uint)v);
        }

        public static void action_long(op_row row, int i, object v)
        {
            bind_int(row, i, (long)v);
        }

        public static void action_ulong(op_row row, int i, object v)
        {
            bind_int(row, i, (long)(ulong)v);
        }

        public static void action_float(op_row row, int i, object v)
        {
            bind_float(row, i, (float)v);
        }
        public static void action_double(op_row row, int i, object v)
        {
            bind_double(row, i, (double)v);
        }

        public static void action_string(op_row row, int i, object v)
        {
            bind_string(row, i, (string)v);
        }

        public static void action_bytes(op_row row, int i, object v)
        {
            bind_binary(row, i, (byte[])v, ((byte[])v).Length);
        }

        public static void loadActions()
        {
            if (!actionLoaded)
            {
                actions.Add(typeof(bool), new Action<op_row, int, object>(action_bool));
                actions.Add(typeof(byte), new Action<op_row, int, object>(action_byte));
                actions.Add(typeof(sbyte), new Action<op_row, int, object>(action_sbyte));
                actions.Add(typeof(short), new Action<op_row, int, object>(action_short));
                actions.Add(typeof(ushort), new Action<op_row, int, object>(action_ushort));
                actions.Add(typeof(int), new Action<op_row, int, object>(action_int));
                actions.Add(typeof(uint), new Action<op_row, int, object>(action_uint));
                actions.Add(typeof(long), new Action<op_row, int, object>(action_long));
                actions.Add(typeof(ulong), new Action<op_row, int, object>(action_ulong));
                actions.Add(typeof(float), new Action<op_row, int, object>(action_float));
                actions.Add(typeof(double), new Action<op_row, int, object>(action_double));
                actions.Add(typeof(string), new Action<op_row, int, object>(action_string));
                actions.Add(typeof(byte[]), new Action<op_row, int, object>(action_bytes));
                actionLoaded = true;
            }
        }
        static bool actionLoaded = false;
        public static Dictionary<Type, Action<op_row, int, object>> actions = new Dictionary<Type, Action<op_row, int, object>>();
    }

    public class TimeConvert
    {
        public TimeConvert() { }

        //DateTime转换为时间戳
        public static double GetTimeSpan(DateTime time)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0, 0));
            return (double)(time - startTime).TotalSeconds + time.Millisecond / 1000.0;
        }

        //timeSpan转换为DateTime
        public static DateTime TimeSpanToDateTime(double span)
        {
            DateTime time = DateTime.MinValue;
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0, 0));
            time = startTime.AddSeconds(span);
            return time;
        }
    }

    public class Connect : IO, IDisposable
    {
        private class Schema
        {
            public static void load(ResultSet resultSet)
            {

                while (resultSet.next())
                {
                    string sqlStr = resultSet.getString("sql");

                    int strBegin = sqlStr.IndexOf('(');
                    int strEnd = sqlStr.IndexOf(')');
                    string strBefore = sqlStr.Substring(0, strBegin - 1);
                    string[] beforeTables = strBefore.Split(' ');
                    string tableName = beforeTables[2];
                    string strIsMy = sqlStr.Substring(strBegin + 1, strEnd - strBegin - 1);
                    string[] mySql = strIsMy.Split(',');
                    string[] strChild;
                    Dictionary<string, int> dic_in = new Dictionary<string, int>();
                    foreach (string s in mySql)
                    {
                        strChild = s.Split(' ');
                        if (strChild.Length < 2)
                        {
                            continue;
                        }
                        switch (strChild[1])
                        {
                            case "int":
                                if (!dic_in.ContainsKey(strChild[0]))
                                {
                                    dic_in.Add(strChild[0], eType.Int32);
                                }
                                break;
                            case "text":
                                if (!dic_in.ContainsKey(strChild[0]))
                                {
                                    dic_in.Add(strChild[0], eType.String);
                                }
                                break;
                            case "datetime":
                                if (!dic_in.ContainsKey(strChild[0]))
                                {
                                    dic_in.Add(strChild[0], eType.DateTime);
                                }
                                break;
                            case "float":
                                if (!dic_in.ContainsKey(strChild[0]))
                                {
                                    dic_in.Add(strChild[0], eType.Float);
                                }
                                break;
                            case "double":
                                if (!dic_in.ContainsKey(strChild[0]))
                                {
                                    dic_in.Add(strChild[0], eType.Double);
                                }
                                break;
                            case "tinyint":
                                if (!dic_in.ContainsKey(strChild[0]))
                                {
                                    dic_in.Add(strChild[0], eType.Int8);
                                }
                                break;
                            case "smallint":
                                if (!dic_in.ContainsKey(strChild[0]))
                                {
                                    dic_in.Add(strChild[0], eType.Int16);
                                }
                                break;
                            case "bigint":
                                if (!dic_in.ContainsKey(strChild[0]))
                                {
                                    dic_in.Add(strChild[0], eType.Int64);
                                }
                                break;
                            case "blob":
                                if (!dic_in.ContainsKey(strChild[0]))
                                {
                                    if (strChild[0] == "AV") // !!!! specific
                                    {
                                        dic_in.Add(strChild[0], eType.Object);
                                    }
                                    else
                                    {
                                        dic_in.Add(strChild[0], eType.Binary);
                                    }
                                }
                                break;
                            default:
                                if (!dic_in.ContainsKey(strChild[0]))
                                {
                                    dic_in.Add(strChild[0], eType.String);
                                }
                                break;
                        }
                    }
                    dic.Add(tableName, dic_in);
                }
                diced = true;
            }

            public static bool diced = false;
            public static Dictionary<string, Dictionary<string, int>> dic = new Dictionary<string, Dictionary<string, int>>();
        }

        // actions
        const int actionInsert = 1;
        const int actionDelete = 2;
        const int actionUpdate = 3;
        const int actionSelect = 4;

        private string ip;
        private int port;
        private int timeout;
        private string user;
        private string password;
        private ResultSet resultSet;

        // 外部非托管资源
        private OpenPlant op;
        private volatile bool disposed = false;
        private Object myLock = new Object();

        public Connect(string ip, int port, int timeout, string user, string password)
        {
            this.ip = ip;
            this.port = port;
            this.timeout = timeout;
            this.user = user;
            this.password = password;
            op = init(0, ip, port, timeout, user, password, "", 0);
            if (op == IntPtr.Zero)
            {
                throw new SystemException("op_init error");
            }
            loadActions();
            loadSchema();
        }

        ~Connect()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (!disposed)
            {
                disposed = true;
                lock (myLock)
                {
                    if (op != IntPtr.Zero)
                    {
                        close(op);
                    }
                }
            }
            GC.SuppressFinalize(this);
        }

        private void loadSchema()
        {
            if (!Schema.diced)
            {
                string sql = "select * from Schema";
                ResultSet rs = executeQuery(sql);
                Schema.load(rs);
                rs.close();
            }
        }

        public void close()
        {
            if (op != IntPtr.Zero)
            {
                close(op);
                op = IntPtr.Zero;
            }
        }

        public bool isAlive()
        {
            return (op != IntPtr.Zero) && status(op) == 0;
        }

        public bool reconnect()
        {
            return (op != IntPtr.Zero) && try_connect(op) == 0;
        }

        private ResultSet execute(OpenPlant op, op_request request)
        {
            string error = "";
            op_response response = IntPtr.Zero;
            op_table result = IntPtr.Zero;

            op_table table_get = get_table(request);
            op_stream opio = get_stream(op);
            set_compress(opio, 1);

            int rv = write_request(opio, request);
            if (rv == 0)
            {
                if (table_get != IntPtr.Zero)
                {
                    rv = write_content(opio, table_get);
                }
                rv = flush_content(opio);
            }

            if (rv != 0)
            {
                error = "write request err";
                throw new SystemException(error);
            }

            rv = get_response(opio, out response);
            if (rv != 0)
            {
                error = "get response error";
                throw new SystemException(error);
            }

            result = get_table(response);

            if (result == IntPtr.Zero)
            {
                error = get_error(response);
                throw new SystemException(error);
            }

            resultSet = new ResultSet(opio, request, response);
            return resultSet;
        }

        private ResultSet modify(int action, string tableName, string[] colNames, object[,] rows, Dictionary<string, object> option = null)
        {
            if (op == IntPtr.Zero)
            {
                throw new SystemException("op_init error");
            }

            int[] typeArray = new int[colNames.Length];
            int colNamesCount = colNames.Length;
            int rowCount = rows.GetLength(0);
            int colCount = rows.GetLength(1);
            if (colNamesCount != colCount)
            {
                throw new SystemException("parameter error");
            }

            op_table table = new_table(tableName);
            string tableName_dic = "";
            if (tableName.IndexOf('.') > 0)
            {
                tableName_dic = tableName.Split('.')[1];
            }
            else
            {
                tableName_dic = tableName;
            }
            tableName_dic = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(tableName_dic);
            try
            {
                for (int i = 0; i < colNamesCount; i++)
                {
                    string cName = colNames[i];
                    int typeNum = Schema.dic[tableName_dic][cName];
                    typeArray[i] = typeNum;
                    add_column(table, cName, (int)typeNum, 0, 0, null, null);
                }
            }
            catch
            {
                throw new SystemException("column not exist");
            }

            try
            {
                for (int i = 0; i < rowCount; i++)
                {
                    op_row row = new_row(table);
                    for (int j = 0; j < colCount; j++)
                    {
                        object v = rows[i, j];
                        if (v == null)
                        {
                            bind_binary(row, j, null, 0);
                            continue;
                        }
                        //if v == null, v.GetType will crash
                        Type t = v.GetType();
                        if (typeArray[j] == eType.DateTime && t == typeof(DateTime))
                        {
                            double second = TimeConvert.GetTimeSpan((DateTime)(v));
                            bind_double(row, j, second);
                        }
                        else if (actions.ContainsKey(t))
                        {
                            Action<op_row, int, object> a = actions[t];
                            a(row, j, v);
                        }
                        else
                        {
                            bind_string(row, j, v.ToString());
                        }
                    }
                    append(table, row);
                }
            }
            catch
            {
                throw new SystemException("row type err");
            }

            op_request request = new_request();
            set_table(request, table);
            set_option(request, "Reqid", "1");
            if (action == actionInsert)
            {
                set_option(request, "Action", "Insert");
            }
            else if (action == actionUpdate)
            {
                set_option(request, "Action", "Update");
            }
            if (option != null && option.Count > 0)
            {
                foreach (KeyValuePair<string, object> item in option)
                {
                    if (item.Value.GetType() == typeof(DateTime))
                    {
                        double second = TimeConvert.GetTimeSpan((DateTime)(item.Value));
                        set_option(request, item.Key, second.ToString());
                    }
                    else
                    {
                        set_option(request, item.Key, item.Value.ToString());
                    }
                }
            }

            return execute(op, request);
        }

        private ResultSet findOrDelete(int action, string tableName, string[] colNames, object[] keys, Dictionary<string, object> option = null)
        {
            string tableName_dic = "";
            if (tableName.IndexOf('.') > 0)
            {
                tableName_dic = tableName.Split('.')[1];
            }
            else
            {
                tableName_dic = tableName;
            }
            if (tableName == "Archive" && option.ContainsValue("stat"))
            {
                tableName_dic = "Stat";
            }
            tableName_dic = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(tableName_dic);
            op_table table = new_table(tableName);
            foreach (string cName in colNames)
            {
                int typeNum = cName == "*" ? 11 : Schema.dic[tableName_dic][cName];
                add_column(table, cName, (int)typeNum, 0, 0, null, null);
            }

            if (action == actionDelete)
            {
                try
                {
                    for (int i = 0; i < keys.Length; i++)
                    {
                        op_row row = new_row(table);
                        object v = keys[i];
                        Type t = v.GetType();
                        if (actions.ContainsKey(t))
                        {
                            Action<op_row, int, object> a = actions[t];
                            a(row, 0, v);
                        }
                        else
                        {
                            bind_string(row, 0, v.ToString());
                        }
                        append(table, row);
                    }
                }
                catch
                {
                    throw new SystemException("keys type must consistent");
                }
            }

            op_request request = new_request();
            set_table(request, table);
            set_option(request, "Reqid", "1");
            if (action == actionSelect)
            {
                set_option(request, "Action", "Select");
                if (option != null && option.Count > 0)
                {
                    foreach (KeyValuePair<string, object> item in option)
                    {
                        if (item.Value.GetType() == typeof(DateTime))
                        {
                            double second = TimeConvert.GetTimeSpan((DateTime)(item.Value));
                            set_option(request, item.Key, second.ToString());
                        }
                        else
                        {
                            set_option(request, item.Key, item.Value.ToString());
                        }
                    }
                }
            }
            else if (action == actionDelete)
            {
                set_option(request, "Action", "Delete");
            }

            int keyLen = 0;
            if (keys != null)
            {
                keyLen = keys.Length;
            }

            if (keyLen > 0 && action == actionSelect)
            {
                string[,] names = new string[1, keyLen];
                long[] ids = new long[keyLen];

                Type t = keys[0].GetType();
                if (t == typeof(int) || t == typeof(long))
                {
                    for (int i = 0; i < keyLen; i++)
                    {
                         ids[i] = (long)keys[i];
                    }
                    set_indices(request, null, keyLen, ids);
                }
                else if (t == typeof(string))
                {
                    for (int i = 0; i < keyLen; i++)
                    {
                        names[0, i] = (string)keys[i];
                    }
                    set_indices_string(request, null, keyLen, names);
                }
            }

            return execute(op, request);
        }

        // exports
        public ResultSet executeQuery(String sql)
        {
            if (disposed)
            {
                throw new ObjectDisposedException("op has been closed");
            }

            if (op == IntPtr.Zero)
            {
                throw new SystemException("op_init error");
            }

            op_request request = new_request();
            set_option(request, "Reqid", "1");
            set_option(request, "Action", "ExecSQL");
            set_option(request, "SQL", sql);

            return execute(op, request);
        }

        public ResultSet insert(string tableName, string[] colNames, object[,] rows, Dictionary<string, object> option = null)
        {
            return modify(actionInsert, tableName, colNames, rows, option);
        }

        public ResultSet update(string tableName, string[] colNames, object[,] rows)
        {
            return modify(actionUpdate, tableName, colNames, rows);
        }

        public ResultSet select(string tableName, string[] colNames, object[] keys, Dictionary<string, object> option = null)
        {
            return findOrDelete(actionSelect, tableName, colNames, keys, option);
        }

        public ResultSet delete(string tableName, string[] colNames, object[] keys)
        {
            return findOrDelete(actionDelete, tableName, colNames, keys, null);
        }

        public Async openAsync(string tableName, event_cb cb, object[] keys)
        {
            op_request async_request = new_request();
            OPAsyncHandler ah;

            op_table table = new_table(tableName);
            add_column(table, "*", eType.Null, 0, 0, null, null);
            set_table(async_request, table);

            int keyLen = keys.Count();
            if (keyLen > 0)
            {
                long[] ids = new long[keyLen];
                string[,] names = new string[1, keyLen];
                Type t = keys[0].GetType();
                if (t == typeof(byte) || t == typeof(Int16) || t == typeof(Int32) || t == typeof(Int64))
                {
                    for (int i = 0; i < keyLen; i++)
                    {
                        ids[i] = long.Parse(keys[i].ToString());
                    }
                    set_indices(async_request, "ID", keyLen, ids);
                }
                else if (t == typeof(string))
                {
                    for (int i = 0; i < keyLen; i++)
                    {
                        names[0, i] = (string)keys[i];
                    }
                    set_indices_string(async_request, "GN", keyLen, names);
                }
                else
                {
                    throw new SystemException("key type err");
                }
            }
            else
            {
                throw new SystemException("keys length must greater than zero");
            }

            set_option(async_request, "Snapshot", "1");

            int error = 0;
            ah = open_async(op, async_request, cb, IntPtr.Zero, out error);
            if (ah == IntPtr.Zero)
            {
                if (async_request != IntPtr.Zero)
                {
                    free_request(async_request);
                }
                throw new SystemException("open async error:" + error.ToString());
            }
            Async a = new Async(ah);
            return a;
        }
    }

    public class ResultSet : IO, IDisposable
    {
        public ResultSet(op_stream opio, op_request request, op_response response)
        {
            this.tableResult = get_table(response);
            this.request = request;
            this.response = response;
            this.opio = opio;
            columnsNum = 0;
            rowsNum = 0;
            pos = -1;
            eof = 0;
        }

        // Finalize只在Dispose方法没被调用的前提下，才能调用执行。
        ~ResultSet()
        {
            Dispose();
        }

        private op_table tableResult = IntPtr.Zero;
        private int columnsNum;
        private int rowsNum;
        private int pos;
        private int eof;
        private op_stream opio;

        // 外部非托管资源
        private op_request request;
        private op_response response;
        private volatile bool disposed = false;
        private Object myLock = new Object();

        public void Dispose()
        {
            if (!disposed)
            {
                disposed = true;
                lock (myLock)
                {
                    if (request != IntPtr.Zero)
                    {
                        free_request(request);
                        request = IntPtr.Zero;
                    }
                    if (response != IntPtr.Zero)
                    {
                        free_response(response);
                        response = IntPtr.Zero;
                    }
                }
            }
            GC.SuppressFinalize(this);
        }

        private bool nextContent()
        {
            if (eof == 0)
            {
                int rv = next_content(opio, tableResult, 1, out eof);
                if (rv != 0)
                {
                    opio = IntPtr.Zero;
                }
                pos = 0;
                rowsNum = row_count(tableResult);
                columnsNum = column_count(tableResult);
            }
            return eof == 0 && pos < rowsNum;
        }

        public int rowCount()
        {
            if (disposed)
            {
                throw new ObjectDisposedException("ResultSet object has been freed");
            }
            return row_count(tableResult);
        }

        public int columnCount()
        {
            if (disposed)
            {
                throw new ObjectDisposedException("ResultSet object has been freed");
            }
            return column_count(tableResult);
        }

        public int columnType(int colNum)
        {
            if (disposed)
            {
                throw new ObjectDisposedException("ResultSet object has been freed");
            }
            int type = column_type(tableResult, colNum);
            return type;
        }

        public string columnTypeName(int colNum)
        {
            if (disposed)
            {
                throw new ObjectDisposedException("ResultSet object has been freed");
            }
            string cType = "";

            int type = column_type(tableResult, colNum);
            switch (type)
            {
                case eType.Null: cType = "null"; break;
                case eType.Bool: cType = "bool"; break;
                case eType.Int8: cType = "byte"; break;
                case eType.Int16: cType = "short"; break;
                case eType.Int32: cType = "int"; break;
                case eType.Int64: cType = "long"; break;
                case eType.Float: cType = "float"; break;
                case eType.Double: cType = "double"; break;
                case eType.DateTime: cType = "DateTime"; break;
                case eType.String: cType = "string"; break;
                case eType.Binary: cType = "byte[]"; break;
                case eType.Object: cType = "object"; break;
                default: cType = "string"; break;
            }
            return cType;
        }

        public string columnLabel(int colNum)
        {
            if (disposed)
            {
                throw new ObjectDisposedException("ResultSet object has been freed");
            }
            string strLab = column_name(tableResult, colNum);

            return strLab;
        }

        public string columnDesc(string colName)
        {
            //string des = "";
            //int col = column_index(tableResult, colName);
            //if (col != -1)
            //{
            //    IntPtr p = column_des(tableResult, col);
            //    des = Marshal.PtrToStringAnsi(p);
            //}
            //return des;
            return "";
        }

        public bool next()
        {
            if (disposed || opio == IntPtr.Zero)
            {
                throw new ObjectDisposedException("ResultSet object has been freed");
            }
            pos++;
            if (pos >= rowsNum)
            {
                if (!nextContent())
                {
                    return false;
                }
            }
            set_rowid(tableResult, pos);
            return true;
        }

        public int valueType(int colNum)
        {
            if (disposed)
            {
                throw new ObjectDisposedException("ResultSet object has been freed");
            }
            int type = value_type(tableResult, colNum);
            return type;
        }

        public bool getBool(string key)
        {
            if (disposed)
            {
                throw new ObjectDisposedException("ResultSet object has been freed");
            }

            long v = 0;
            int col = column_index(tableResult, key);
            if (col != -1)
            {
                v = column_int(tableResult, col);
            }
            else
            {
                return false;
            }
            return (v != 0);
        }

        public SByte getByte(string key)
        {
            long l = getLong(key);
            return (SByte)(l & 0xffL);
        }

        public byte[] getBytes(string key)
        {
            if (disposed)
            {
                throw new ObjectDisposedException("ResultSet object has been freed");
            }

            byte[] val = null;
            int col = column_index(tableResult, key);
            if (col != -1)
            {
                val = column_binary(tableResult, col);
            }
            else
            {
                return null;
            }
            return val;
        }

        public Int16 getShort(string key)
        {
            long l = getLong(key);
            return (Int16)(l & 0xffffL);
        }

        public int getInt(string key)
        {
            long l = getLong(key);
            return (int)(l & 0xffffffffL);
        }

        public long getLong(string key)
        {
            if (disposed)
            {
                throw new ObjectDisposedException("ResultSet object has been freed");
            }

            long num = -1;
            int col = column_index(tableResult, key);
            if (col != -1)
            {
                num = column_int(tableResult, col);
            }
            else
            {
                Nullable<long> a = null;
                return a.Value;
            }
            return num;
        }

        public float getFloat(string key)
        {
            if (disposed)
            {
                throw new ObjectDisposedException("ResultSet object has been freed");
            }

            double num = 0.0;
            int col = column_index(tableResult, key);
            if (col != -1)
            {
                num = column_double(tableResult, col);
            }
            else
            {
                return float.NaN;
            }
            return (float)num;
        }

        public double getDouble(string key)
        {
            if (disposed)
            {
                throw new ObjectDisposedException("ResultSet object has been freed");
            }

            double num = 0.0;
            int col = column_index(tableResult, key);
            if (col != -1)
            {
                num = column_double(tableResult, col);
            }
            else
            {
                return double.NaN;
            }
            return num;
        }

        public DateTime getDateTime(string key)
        {
            if (disposed)
            {
                throw new ObjectDisposedException("ResultSet object has been freed");
            }

            double num = 0.0;
            int col = column_index(tableResult, key);
            if (col != -1)
            {
                num = column_double(tableResult, col);
            }
            else
            {
                return new DateTime();
            }
            return TimeConvert.TimeSpanToDateTime(num);
        }

        public string getString(string key)
        {
            if (disposed)
            {
                throw new ObjectDisposedException("ResultSet object has been freed");
            }

            string val = "";
            int col = column_index(tableResult, key);
            if (col != -1)
            {
                val = column_string(tableResult, col);
            }
            else
            {
                return null;
            }
            return val;
        }

        public DateTime getDatetime(string key)
        {
            if (disposed)
            {
                throw new ObjectDisposedException("ResultSet object has been freed");
            }

            double dTime = 0.0;
            int col = column_index(tableResult, key);
            if (col != -1)
            {
                dTime = column_double(tableResult, col);
            }
            else
            {
                return new DateTime();
            }
            return TimeConvert.TimeSpanToDateTime(dTime);
        }

        public SByte getByte(int colNum)
        {
            if (disposed)
            {
                throw new ObjectDisposedException("ResultSet object has been freed");
            }
            long num = column_int(tableResult, colNum);
            return (SByte)(num & 0xffL);
        }

        public Int16 getShort(int colNum)
        {
            if (disposed)
            {
                throw new ObjectDisposedException("ResultSet object has been freed");
            }
            long num = column_int(tableResult, colNum);
            return (Int16)(num & 0xffffL);
        }

        public int getInt(int colNum)
        {
            if (disposed)
            {
                throw new ObjectDisposedException("ResultSet object has been freed");
            }

            long num = column_int(tableResult, colNum);
            return (int)(num & 0xffffffffL);
        }

        public long getLong(int colNum)
        {
            if (disposed)
            {
                throw new ObjectDisposedException("ResultSet object has been freed");
            }

            long num = column_int(tableResult, colNum);
            return num;
        }

        public double getDouble(int colNum)
        {
            if (disposed)
            {
                throw new ObjectDisposedException("ResultSet object has been freed");
            }

            double num = column_double(tableResult, colNum);
            return num;
        }

        public string getString(int colNum)
        {
            if (disposed)
            {
                throw new ObjectDisposedException("ResultSet object has been freed");
            }

            string val = column_string(tableResult, colNum);
            return val;
        }

        public bool getBool(int colNum)
        {
            long v = column_int(tableResult, colNum);
            return (v != 0);
        }

        public float getFloat(int colNum)
        {
            if (disposed)
            {
                throw new ObjectDisposedException("ResultSet object has been freed");
            }

            double num = column_double(tableResult, colNum);
            return (float)num;
        }

        public DateTime getDateTime(int colNum)
        {
            if (disposed)
            {
                throw new ObjectDisposedException("ResultSet object has been freed");
            }

            double dTime = column_double(tableResult, colNum);
            return TimeConvert.TimeSpanToDateTime(dTime);
        }

        public object getObject(int colNum)
        {
            if (disposed)
            {
                throw new ObjectDisposedException("ResultSet object has been freed");
            }
            object value;
            int type = value_type(tableResult, colNum);
            switch (type)
            {
                case eType.Bool:
                case eType.Int8:
                case eType.Int16:
                case eType.Int32:
                case eType.Int64:
                    value = column_int(tableResult, colNum);
                    break;
                case eType.Float:
                case eType.Double:
                    value = column_double(tableResult, colNum);
                    break;
                case eType.Binary:
                    value = column_binary(tableResult, colNum);
                    break;
                case eType.DateTime:
                    double dtime = column_double(tableResult, colNum);
                    DateTime dt = TimeConvert.TimeSpanToDateTime(dtime);
                    value = dt;
                    break;
                default:
                    value = column_string(tableResult, colNum);
                    break;
            }
            return value;
        }

        public void close()
        {
            Dispose();
        }
    }

    public class Async : IO, IDisposable
    {
        public Async(OPAsyncHandler ah)
        {
            if (ah != IntPtr.Zero)
            {
                this.ah = ah;
            }
        }

        // Finalize只在Dispose方法没被调用的前提下，才能调用执行。
        ~Async()
        {
            Dispose();
        }

        // 外部非托管资源
        private OPAsyncHandler ah;
        private volatile bool disposed = false;
        private Object myLock = new Object();

        public void Dispose()
        {
            if (!disposed)
            {
                disposed = true;
                lock (myLock)
                {
                    close();
                }
            }
            GC.SuppressFinalize(this);
        }

        public void close()
        {
            if (ah != IntPtr.Zero)
            {
                close_async(ah);
                ah = IntPtr.Zero;
            }
        }

        public void add(object[] keys)
        {
            subscribe(keys, 1);
        }

        public void remove(object[] keys)
        {
            subscribe(keys, 0);
        }

        private int subscribe(object[] keys, int onoff)
        {
            int isSuccess = -1;
            int keyLen = keys.Count();
            if (keyLen > 0)
            {
                bool keyIsID = false;
                long[] ids = new long[keyLen];
                string[] names = new string[keyLen];
                Type t = keys[0].GetType();

                if (t == typeof(byte) || t == typeof(Int16) || t == typeof(Int32) || t == typeof(Int64))
                {
                    for (int i = 0; i < keyLen; i++)
                    {
                        ids[i] = long.Parse(keys[i].ToString());
                    }
                    keyIsID = true;
                }
                else if (t == typeof(string))
                {
                    for (int i = 0; i < keyLen; i++)
                    {
                        names[i] = (string)keys[i];
                    }
                    keyIsID = false;
                }
                else
                {
                    throw new SystemException("key type err");
                }

                if (keyIsID)
                {
                    isSuccess = async_subscribe(ah, keyLen, ids, onoff);
                }
                else
                {
                    isSuccess = async_subscribe_tags(ah, keyLen, names, onoff);
                }
            }
            else
            {
                throw new SystemException("keys length must greater than zero");
            }

            return isSuccess;
        }


    }
}

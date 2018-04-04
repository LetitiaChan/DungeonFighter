using System.Collections.Generic;
using System;
using System.IO;
using System.Threading;

namespace Kernal
{
    /// <summary>
    /// 日志调试系统（Log日志）
    /// Description:
    ///     1> 作用：更方便于软件（游戏）开发人员，调试系统程序
    ///     2> 基本实现原理：
    ///         1：把开发人员在代码中定义的调试语句，写入本日志的“缓存”。
    ///         2：当缓存中数量超过定义的最大写入文件数值，则把缓存内容调试语句一次性写入文本文件。
    /// </summary>
    public static class Log
    {
        private static List<string> _LogArray;
        private static string _LogPath = null;
        private static State _LogState;
        private static int _LogMaxCapacity;
        private static int _LogBufferMaxNumber;

        private const string XML_CONFIG_LOG_PATH = "LogPath";
        private const string XML_CONFIG_LOG_STATE = "LogState";
        private const string XML_CONFIG_LOG_MAX_CAPACITY = "LogMaxCapacity";
        private const string XML_CONFIG_LOG_BUFFER_NUMBER = "LogBufferNumber";

        private const string XML_CONFIG_LOG_STATE_DEVELOP = "Develop";
        private const string XML_CONFIG_LOG_STATE_SPECIAL = "Speacial";
        private const string XML_CONFIG_LOG_STATE_DEPLOY = "Deploy";
        private const string XML_CONFIG_LOG_STATE_STOP = "Stop";

        private static string XML_CONFIG_LOG_DEFAULT_PATH = "DungeonFighterLog.txt";
        private static int LOG_DEFAULT_MAX_CACITY_NUMBER = 2000;
        private static int LOG_DEFAULT_MAX_LOG_BUFFER_NUMBER = 1;
        private static string LOG_TIPS = "@@@ Important  !!!   ";

        private static string logState = null;
        private static string logMaxCapacity = null;
        private static string logBufferNumber = null;



        static Log()
        {
            _LogArray = new List<string>();

#if UNITY_STANDALONE_WIN || UNITY_EDITOR
            IConfigManger configMgr = new ConfigManager(KernalParameter.GetLogPath(), KernalParameter.GetLogRootNodeName());
            _LogPath = configMgr.AppSetting[XML_CONFIG_LOG_PATH];
            logState = configMgr.AppSetting[XML_CONFIG_LOG_STATE];
            logMaxCapacity = configMgr.AppSetting[XML_CONFIG_LOG_MAX_CAPACITY];
            logBufferNumber = configMgr.AppSetting[XML_CONFIG_LOG_BUFFER_NUMBER];
#endif

            if (string.IsNullOrEmpty(_LogPath))
            {
                _LogPath = UnityEngine.Application.persistentDataPath + "\\" + XML_CONFIG_LOG_DEFAULT_PATH;
            }

            if (!string.IsNullOrEmpty(logState))
            {
                switch (logState)
                {
                    case XML_CONFIG_LOG_STATE_DEVELOP:
                        _LogState = State.Develop;
                        break;
                    case XML_CONFIG_LOG_STATE_SPECIAL:
                        _LogState = State.Speacial;
                        break;
                    case XML_CONFIG_LOG_STATE_DEPLOY:
                        _LogState = State.Deploy;
                        break;
                    case XML_CONFIG_LOG_STATE_STOP:
                        _LogState = State.Stop;
                        break;
                    default:
                        _LogState = State.Stop;
                        break;
                }
            }
            else
            {
                _LogState = State.Stop;
            }

            if (!string.IsNullOrEmpty(logMaxCapacity))
            {
                _LogMaxCapacity = Convert.ToInt32(logMaxCapacity);
            }
            else
            {
                _LogMaxCapacity = LOG_DEFAULT_MAX_CACITY_NUMBER;
            }

            if (!string.IsNullOrEmpty(logBufferNumber))
            {
                _LogBufferMaxNumber = Convert.ToInt32(logBufferNumber);
            }
            else
            {
                _LogBufferMaxNumber = LOG_DEFAULT_MAX_LOG_BUFFER_NUMBER;
            }

#if UNITY_STANDALONE_WIN || UNITY_EDITOR
            if (!File.Exists(_LogPath))
            {
                File.Create(_LogPath);
                Thread.CurrentThread.Abort();                                  //终止当前线程
            }
            SyncFileDataToLogArray();
#endif
        }

        private static void SyncFileDataToLogArray()
        {
            if (!string.IsNullOrEmpty(_LogPath))
            {
                StreamReader sr = new StreamReader(_LogPath);
                while (sr.Peek() >= 0)
                {
                    _LogArray.Add(sr.ReadLine());
                }
                sr.Close();
            }
        }

        public static void Write(string writeFileDate, Level level)
        {
            if (_LogState == State.Stop) { return; }

            if (_LogArray.Count >= _LogMaxCapacity)
            {
                _LogArray.Clear();
            }

            if (!string.IsNullOrEmpty(writeFileDate))
            {
                writeFileDate = "Log State:" + _LogState.ToString() + "/" + DateTime.Now.ToString() + "/" + writeFileDate;

                if (level == Level.High)
                {
                    writeFileDate = LOG_TIPS + writeFileDate;
                }
                switch (_LogState)
                {
                    case State.Develop:
                        AppendDateToFile(writeFileDate);
                        break;
                    case State.Speacial:
                        if (level == Level.High || level == Level.Special)
                        {
                            AppendDateToFile(writeFileDate);
                        }
                        break;
                    case State.Deploy:
                        if (level == Level.High)
                        {
                            AppendDateToFile(writeFileDate);
                        }
                        break;
                    case State.Stop:
                        break;
                    default:
                        break;
                }
            }
        }

        public static void Write(string writeFileDate)
        {
            Write(writeFileDate, Level.Low);
        }

        private static void AppendDateToFile(string writeFileDate)
        {
            if (!string.IsNullOrEmpty(writeFileDate))
            {
                _LogArray.Add(writeFileDate);
            }

            if (_LogArray.Count % _LogBufferMaxNumber == 0)
            {
                SyncLogArrayToFile();
            }
        }


        #region  重要管理方法

        public static List<string> QueryAllDateFromLogBuffer()
        {
            if (_LogArray != null)
            {
                return _LogArray;
            }
            else
            {
                return null;
            }
        }

        public static void ClearLogFileAndBufferAllDate()
        {
            if (_LogArray != null)
            {
                _LogArray.Clear();
            }
            SyncLogArrayToFile();
        }

        public static void SyncLogArrayToFile()
        {
            if (!string.IsNullOrEmpty(_LogPath))
            {
                StreamWriter sw = new StreamWriter(_LogPath);
                foreach (string item in _LogArray)
                {
                    sw.WriteLine(item);
                }
                sw.Close();
            }
        }

        #endregion

        #region  本类的枚举类型
        /// <summary>
        /// 日志状态（部署模式）
        /// </summary>
        public enum State
        {
            Develop,                                                           //开发模式（输出所有日志内容）
            Speacial,                                                          //指定输出模式
            Deploy,                                                            //部署模式（只输出最核心日志信息，例如严重错误信息，用户登陆账号等）
            Stop                                                               //停止输出模式（不输出任何日志信息）
        };

        /// <summary>
        /// 调试信息的等级（表示调试信息本身的重要程度）
        /// </summary>
        public enum Level
        {
            High,
            Special,
            Low
        }
        #endregion
    }
}

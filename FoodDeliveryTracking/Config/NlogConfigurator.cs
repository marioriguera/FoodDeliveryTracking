using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;

namespace FoodDeliveryTracking.Config
{
    /// <summary>
    /// NLogger configuration.
    /// </summary>
    public class NlogConfigurator
    {
        /// <summary>
        /// Gets or sets NLog logging configuration.
        /// </summary>
        private static LoggingConfiguration Config { get; set; }

        /// <summary>
        /// Adds ColoredConsole log target to NLog.
        /// </summary>
        public static void AddConsole()
        {
            ColoredConsoleTarget logConsole = LogManager.Configuration.FindTargetByName<ColoredConsoleTarget>("CONSOLELOG");

            // Ensure target created
            if (logConsole == null)
            {
                logConsole = new ColoredConsoleTarget("CONSOLELOG");
                LogManager.Configuration.AddRule(ApiConfiguration.Current.LogLevel, NLog.LogLevel.Fatal, logConsole);
            }

            // Refresh log levels
            foreach (var rule in LogManager.Configuration.LoggingRules.Where(x => x.Targets.Contains(logConsole)))
            {
                rule.SetLoggingLevels(ApiConfiguration.Current.LogLevel, NLog.LogLevel.Fatal);
            }

            // Refresh layout
            logConsole.Layout = new SimpleLayout("${date} | ${pad:padding=-8:inner=${level:uppercase=true}} | ${processname} | ${pad:padding=5:inner=${mdc:item=CYCLE}} | ${pad:padding=-30:inner=${mdc:item=THREADNAME:whenEmpty=${threadname:whenEmpty=${logger}}}} | ${pad:padding=-45:inner=${mdc:item=CALLERNAME:whenEmpty=${callsite}}} | ${pad:padding=10:inner=${mdc:item=IDX}} | ${pad:padding=7:inner=${mdc:item=KNR}} | ${message}");
        }

        /// <summary>
        /// Adds Debugger log to NLog.
        /// </summary>
        public static void AddDebugger()
        {
            DebuggerTarget logDebugger = LogManager.Configuration.FindTargetByName<DebuggerTarget>("DEBUGGERLOG");

            // Ensure target created
            if (logDebugger == null)
            {
                logDebugger = new DebuggerTarget("DEBUGGERLOG");
                LogManager.Configuration.AddRule(ApiConfiguration.Current.LogLevel, NLog.LogLevel.Fatal, logDebugger);
            }

            // Refresh log levels
            foreach (var rule in LogManager.Configuration.LoggingRules.Where(x => x.Targets.Contains(logDebugger)))
            {
                rule.SetLoggingLevels(ApiConfiguration.Current.LogLevel, NLog.LogLevel.Fatal);
            }

            // Refresh layout
            logDebugger.Layout = new SimpleLayout("${date} | ${pad:padding=-8:inner=${level:uppercase=true}} | ${processname} | ${pad:padding=5:inner=${mdc:item=CYCLE}} | ${pad:padding=-30:inner=${mdc:item=THREADNAME:whenEmpty=${threadname:whenEmpty=${logger}}}} | ${pad:padding=-45:inner=${mdc:item=CALLERNAME:whenEmpty=${callsite}}} | ${pad:padding=10:inner=${mdc:item=IDX}} | ${pad:padding=7:inner=${mdc:item=KNR}} | ${message}");
        }

        /// <summary>
        /// Adds rolling file log target to NLog.
        /// </summary>
        public static void AddRollingFile()
        {
            FileTarget logFile = LogManager.Configuration.FindTargetByName<FileTarget>("ROLLINGFILELOG");

            // Ensure target created
            if (logFile == null)
            {
                logFile = new FileTarget("ROLLINGFILELOG");
                LogManager.Configuration.AddRule(ApiConfiguration.Current.LogLevel, NLog.LogLevel.Fatal, logFile);
            }

            string fileName = ApiConfiguration.Current.LogPath;
            string archiveName = Path.Combine(Path.GetDirectoryName(ApiConfiguration.Current.LogPath) ?? string.Empty, Path.GetFileNameWithoutExtension(ApiConfiguration.Current.LogPath) + ".zip");
            logFile.ArchiveFileKind = FilePathKind.Unknown;
            logFile.ArchiveOldFileOnStartup = false;
            logFile.ArchiveFileName = archiveName;
            logFile.ArchiveNumbering = ArchiveNumberingMode.DateAndSequence;
            logFile.ArchiveDateFormat = "yyyyMMdd";
            logFile.ArchiveAboveSize = ApiConfiguration.Current.LogMaxFileSize;
            logFile.MaxArchiveFiles = 31;
            logFile.EnableArchiveFileCompression = true;
            logFile.FileName = fileName;
            logFile.FileNameKind = FilePathKind.Unknown;

            // Refresh log levels
            foreach (var rule in LogManager.Configuration.LoggingRules.Where(x => x.Targets.Contains(logFile)))
            {
                rule.SetLoggingLevels(ApiConfiguration.Current.LogLevel, NLog.LogLevel.Fatal);
            }

            // Refresh layout
            logFile.Layout = new SimpleLayout("${date} | ${pad:padding=-8:inner=${level:uppercase=true}} | ${processname} | ${pad:padding=5:inner=${mdc:item=CYCLE}} | ${pad:padding=-30:inner=${mdc:item=THREADNAME:whenEmpty=${threadname:whenEmpty=${logger}}}} | ${pad:padding=-45:inner=${mdc:item=CALLERNAME:whenEmpty=${callsite}}} | ${pad:padding=10:inner=${mdc:item=IDX}} | ${pad:padding=7:inner=${mdc:item=KNR}} | ${message}");
        }

        /// <summary>
        /// Initializes the NLog logger.
        /// </summary>
        public static void Initialize()
        {
            if (Config == null)
            {
                Config = new LoggingConfiguration();
                LogManager.Configuration = Config;
            }

            // Clear rules
            Config.LoggingRules.Clear();
        }

        /// <summary>
        /// Starts or reload loggers to apply reconfiguration.
        /// </summary>
        public static void Start()
        {
            LogManager.Configuration.Reload();
            LogManager.ReconfigExistingLoggers();
            LogManager.GetCurrentClassLogger().Trace("NLog reloaded.");
        }

        /// <summary>
        /// Reconfigures all loggers with configuration from <see cref="ApiConfiguration"/>.
        /// </summary>
        public static void ApplyConfigurationToLogs()
        {
            if (Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") == "Development")
            {
                AddDebugger();
            }

            if (!string.IsNullOrWhiteSpace(ApiConfiguration.Current.LogPath))
            {
                AddRollingFile();
            }

            Start();
        }
    }
}

//
// Copyright (c) 2023 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/zenn-search/blob/main/LICENSE
//

using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.ZennSearch.Logging
{

    public static class LoggerExtensions
    {

        private static readonly Action<ILogger, Exception?> unhandledError =
            LoggerMessage.Define(
                LogLevel.Error,
                new EventId(1),
                "予期しない問題が発生しました。"
            );

        public static void UnhandledError(this ILogger logger, Exception? exception)
        {
            unhandledError.Invoke(logger, exception);
        }

        private static readonly Action<ILogger, Exception?> indexStarted =
            LoggerMessage.Define(
                LogLevel.Information,
                new EventId(1001),
                "インデックスを開始しました。"
            );

        public static void IndexStarted(this ILogger logger)
        {
            indexStarted.Invoke(logger, null);
        }

        private static readonly Action<ILogger, Exception?> indexEnded =
            LoggerMessage.Define(
                LogLevel.Information,
                new EventId(1002),
                "インデックスを終了しました。"
            );

        public static void IndexEnded(this ILogger logger)
        {
            indexEnded.Invoke(logger, null);
        }

        private static readonly Action<ILogger, string, string, Exception?> indexExecuted =
            LoggerMessage.Define<string, string>(
                LogLevel.Information,
                new EventId(1003),
                "インデックスを実行しました。Name: {Name} ETag: {ETag}"
            );

        public static void IndexExecuted(this ILogger logger, string name, string etag)
        {
            indexExecuted.Invoke(logger, name, etag, null);
        }

        private static readonly Action<ILogger, string, string, Exception?> indexSkipped =
            LoggerMessage.Define<string, string>(
                LogLevel.Information,
                new EventId(1004),
                "インデックスをスキップしました。Name: {Name} ETag: {ETag}"
            );

        public static void IndexSkipped(this ILogger logger, string name, string etag)
        {
            indexSkipped.Invoke(logger, name, etag, null);
        }

        private static readonly Action<ILogger, Exception?> searchStarted =
            LoggerMessage.Define(
                LogLevel.Information,
                new EventId(1001),
                "検索を開始しました。"
            );

        public static void SearchStarted(this ILogger logger)
        {
            searchStarted.Invoke(logger, null);
        }

        private static readonly Action<ILogger, Exception?> searchEnded =
            LoggerMessage.Define(
                LogLevel.Information,
                new EventId(1002),
                "検索を終了しました。"
            );

        public static void SearchEnded(this ILogger logger)
        {
            searchEnded.Invoke(logger, null);
        }

    }

}

//
// Copyright (c) 2023-2024 karamem0
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

        private static readonly Action<ILogger, Exception?> creationStarted =
            LoggerMessage.Define(
                LogLevel.Information,
                new EventId(1001),
                "インデックスの作成を開始しました。"
            );

        public static void CreationStarted(this ILogger logger)
        {
            creationStarted.Invoke(logger, null);
        }

        private static readonly Action<ILogger, Exception?> creationEnded =
            LoggerMessage.Define(
                LogLevel.Information,
                new EventId(1002),
                "インデックスの作成を終了しました。"
            );

        public static void CreationEnded(this ILogger logger)
        {
            creationEnded.Invoke(logger, null);
        }

        private static readonly Action<ILogger, string, string, Exception?> creationExecuted =
            LoggerMessage.Define<string, string>(
                LogLevel.Information,
                new EventId(1003),
                "インデックスの作成を実行しました。Name: {Name} ETag: {ETag}"
            );

        public static void CreationExecuted(this ILogger logger, string name, string etag)
        {
            creationExecuted.Invoke(logger, name, etag, null);
        }

        private static readonly Action<ILogger, string, string, Exception?> creationSkipped =
            LoggerMessage.Define<string, string>(
                LogLevel.Information,
                new EventId(1004),
                "インデックスの作成をスキップしました。Name: {Name} ETag: {ETag}"
            );

        public static void CreationSkipped(this ILogger logger, string name, string etag)
        {
            creationSkipped.Invoke(logger, name, etag, null);
        }

        private static readonly Action<ILogger, Exception?> deletionStarted =
            LoggerMessage.Define(
                LogLevel.Information,
                new EventId(2001),
                "インデックスの削除を開始しました。"
            );

        public static void DeletionStarted(this ILogger logger)
        {
            deletionStarted.Invoke(logger, null);
        }

        private static readonly Action<ILogger, Exception?> deletionEnded =
            LoggerMessage.Define(
                LogLevel.Information,
                new EventId(2002),
                "インデックスの削除を終了しました。"
            );

        public static void DeletionEnded(this ILogger logger)
        {
            deletionEnded.Invoke(logger, null);
        }

        private static readonly Action<ILogger, Exception?> searchStarted =
            LoggerMessage.Define(
                LogLevel.Information,
                new EventId(3001),
                "インデックスの検索を開始しました。"
            );

        public static void SearchStarted(this ILogger logger)
        {
            searchStarted.Invoke(logger, null);
        }

        private static readonly Action<ILogger, Exception?> searchEnded =
            LoggerMessage.Define(
                LogLevel.Information,
                new EventId(3002),
                "インデックスの検索を終了しました。"
            );

        public static void SearchEnded(this ILogger logger)
        {
            searchEnded.Invoke(logger, null);
        }

    }

}

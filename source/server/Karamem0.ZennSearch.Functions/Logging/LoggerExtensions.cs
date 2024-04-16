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
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.ZennSearch.Logging;

public static class LoggerExtensions
{

    private static readonly Action<ILogger, string, Exception?> unhandledError =
        LoggerMessage.Define<string>(
            LogLevel.Critical,
            new EventId(1),
            "予期しない問題が発生しました。Error: {ErrorMessage}"
        );

    public static void UnhandledError(this ILogger logger, string errorMessage, Exception? exception)
    {
        unhandledError.Invoke(logger, errorMessage, exception);
    }

    private static readonly Action<ILogger, string, Exception?> invalidOperationError =
        LoggerMessage.Define<string>(
            LogLevel.Error,
            new EventId(2),
            "不正な操作を行おうとしました。Error: {ErrorMessage}"
        );

    public static void InvalidOperationError(this ILogger logger, string errorMessage, Exception? exception)
    {
        invalidOperationError.Invoke(logger, errorMessage, exception);
    }

    private static readonly Action<ILogger, string?, Exception?> creationStarted =
        LoggerMessage.Define<string?>(
            LogLevel.Information,
            new EventId(1001),
            "インデックスの作成を開始しました。MemberName: {MemberName}"
        );

    public static void CreationStarted(this ILogger logger, [CallerMemberName()] string? memberName = null)
    {
        creationStarted.Invoke(logger, memberName, null);
    }

    private static readonly Action<ILogger, string?, Exception?> creationEnded =
        LoggerMessage.Define<string?>(
            LogLevel.Information,
            new EventId(1002),
            "インデックスの作成を終了しました。MemberName: {MemberName}"
        );

    public static void CreationEnded(this ILogger logger, [CallerMemberName()] string? memberName = null)
    {
        creationEnded.Invoke(logger, memberName, null);
    }

    private static readonly Action<ILogger, string, string, string?, Exception?> creationExecuted =
        LoggerMessage.Define<string, string, string?>(
            LogLevel.Information,
            new EventId(1003),
            "インデックスの作成を実行しました。Name: {Name}, ETag: {ETag}, MemberName: {MemberName}"
        );

    public static void CreationExecuted(this ILogger logger, string name, string etag, [CallerMemberName()] string? memberName = null)
    {
        creationExecuted.Invoke(logger, name, etag, memberName, null);
    }

    private static readonly Action<ILogger, string, string, string?, Exception?> creationSkipped =
        LoggerMessage.Define<string, string, string?>(
            LogLevel.Information,
            new EventId(1004),
            "インデックスの作成をスキップしました。Name: {Name}, ETag: {ETag}, MemberName: {MemberName}"
        );

    public static void CreationSkipped(this ILogger logger, string name, string etag, [CallerMemberName()] string? memberName = null)
    {
        creationSkipped.Invoke(logger, name, etag, memberName, null);
    }

    private static readonly Action<ILogger, string?, Exception?> deletionStarted =
        LoggerMessage.Define<string?>(
            LogLevel.Information,
            new EventId(2001),
            "インデックスの削除を開始しました。MemberName: {MemberName}"
        );

    public static void DeletionStarted(this ILogger logger, [CallerMemberName()] string? memberName = null)
    {
        deletionStarted.Invoke(logger, memberName, null);
    }

    private static readonly Action<ILogger, string?, Exception?> deletionEnded =
        LoggerMessage.Define<string?>(
            LogLevel.Information,
            new EventId(2002),
            "インデックスの削除を終了しました。MemberName: {MemberName}"
        );

    public static void DeletionEnded(this ILogger logger, [CallerMemberName()] string? memberName = null)
    {
        deletionEnded.Invoke(logger, memberName, null);
    }

    private static readonly Action<ILogger, string, int, string?, Exception?> searchStarted =
        LoggerMessage.Define<string, int, string?>(
            LogLevel.Information,
            new EventId(3001),
            "インデックスの検索を開始しました。Query: {Query}, Count: {Count}, MemberName: {MemberName}"
        );

    public static void SearchStarted(this ILogger logger, string query, int count, [CallerMemberName()] string? memberName = null)
    {
        searchStarted.Invoke(logger, query, count, memberName, null);
    }

    private static readonly Action<ILogger, string?, Exception?> searchEnded =
        LoggerMessage.Define<string?>(
            LogLevel.Information,
            new EventId(3002),
            "インデックスの検索を終了しました。MemberName: {MemberName}"
        );

    public static void SearchEnded(this ILogger logger, [CallerMemberName()] string? memberName = null)
    {
        searchEnded.Invoke(logger, memberName, null);
    }

}

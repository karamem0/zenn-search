//
// Copyright (c) 2023-2024 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/zenn-search/blob/main/LICENSE
//

declare module 'ress';

declare namespace NodeJS {

  interface ProcessEnv {
    readonly VITE_TELEMETRY_CONNECTION_STRING: string,
    readonly VITE_SEARCH_API_KEY: string,
    readonly VITE_SEARCH_API_URL: string,
    readonly VITE_ZENN_URL: string
  }

}

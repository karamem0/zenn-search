//
// Copyright (c) 2023-2024 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/zenn-search/blob/main/LICENSE
//

interface ServiceConfig {
  searchApiKey: string,
  searchApiUrl: string
}

export const config: ServiceConfig = {
  searchApiKey: process.env.VITE_SEARCH_API_KEY,
  searchApiUrl: process.env.VITE_SEARCH_API_URL
};

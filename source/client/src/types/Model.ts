//
// Copyright (c) 2023-2024 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/zenn-search/blob/main/LICENSE
//

export interface IndexDataDTO {
  title: string,
  emoji: string,
  content: string,
  created: string,
  updated: string,
  etag: string
}

export interface SearchIndexDataDTO {
  '@@score': number,
  id: string,
  value?: IndexDataDTO
}

export interface SearchIndexData {
  score: number,
  id: string,
  title: string,
  emoji: string,
  content: string,
  published: string
}

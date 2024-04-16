//
// Copyright (c) 2023-2024 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/zenn-search/blob/main/LICENSE
//

import { mapper } from '../mappings/AutoMapperProfile';
import {
  SearchIndexData,
  SearchIndexDataDTO,
  SearchTarget
} from '../types/Model';

export async function searchIndex(target: SearchTarget, query: string, count: number = 10): Promise<SearchIndexData[]> {
  return await fetch(
    `${process.env.VITE_SEARCH_API_URL}?target=${target}&query=${query}&count=${count}`,
    {
      method: 'GET',
      mode: 'cors',
      headers: {
        'X-Functions-Key': process.env.VITE_SEARCH_API_KEY
      }
    })
    .then((response) => response.status === 200 ? response : Promise.reject(response))
    .then((response) => response.json())
    .then((json) => json.value as SearchIndexDataDTO[])
    .then((data) => mapper.mapArray(data, 'SearchIndexDataDTO', 'SearchIndexData'));
}

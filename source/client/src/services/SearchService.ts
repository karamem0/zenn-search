//
// Copyright (c) 2023 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/zenn-search/blob/main/LICENSE
//

import { mapper } from '../mappings/AutoMapperProfile';
import { SearchIndexData, SearchIndexDataDTO } from '../types/Model';

export async function searchIndex(query: string, count: number = 10): Promise<SearchIndexData[]> {
  return await fetch(
    `${import.meta.env.VITE_SEARCH_API_URL}` +
    `?query=${query}` +
    `&count=${count}`,
    {
      method: 'GET',
      mode: 'cors',
      headers: {
        'X-Functions-Key': import.meta.env.VITE_SEARCH_API_KEY
      }
    })
    .then((response) => response.status === 200 ? response : Promise.reject(response))
    .then((response) => response.json())
    .then((json) => json.value as SearchIndexDataDTO[])
    .then((data) => mapper.mapArray(data, 'SearchIndexDataDTO', 'SearchIndexData'));
}

//
// Copyright (c) 2023-2024 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/zenn-search/blob/main/LICENSE
//

import { SearchTarget } from './Model';

export interface TargetDropdownOption {
    text: string,
    value: string
  }

export const targetDropdownOptions: TargetDropdownOption[] = [
  {
    text: '(指定しない)',
    value: SearchTarget.both
  },
  {
    text: 'Azure AI Search (HNSW)',
    value: SearchTarget.aisearch
  },
  {
    text: 'Azure Cosmos DB for MongoDB (IVF)',
    value: SearchTarget.mongodb
  }
];

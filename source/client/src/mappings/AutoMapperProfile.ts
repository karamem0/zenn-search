//
// Copyright (c) 2023-2024 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/zenn-search/blob/main/LICENSE
//

import {
  createMap,
  createMapper,
  forMember,
  mapFrom
} from '@automapper/core';
import { pojos, PojosMetadataMap } from '@automapper/pojos';

import {
  IndexDataDTO,
  SearchIndexData,
  SearchIndexDataDTO
} from '../types/Model';

export const mapper = createMapper({
  strategyInitializer: pojos()
});

PojosMetadataMap.create<IndexDataDTO>('IndexDataDTO', {
  title: String,
  content: String,
  created: String,
  updated: String,
  etag: String
});

PojosMetadataMap.create<SearchIndexDataDTO>('SearchIndexDataDTO', {
  '@search.score': Number,
  id: String,
  value: 'IndexDataDTO'
});

PojosMetadataMap.create<SearchIndexData>('SearchIndexData', {
  score: Number,
  id: String,
  title: String,
  content: String,
  published: String
});

createMap<SearchIndexDataDTO, SearchIndexData>(
  mapper,
  'SearchIndexDataDTO',
  'SearchIndexData',
  forMember((target) => target.score, mapFrom((source) => source['@search.score'])),
  forMember((target) => target.title, mapFrom((source) => source.value?.title)),
  forMember((target) => target.emoji, mapFrom((source) => source.value?.emoji)),
  forMember((target) => target.content, mapFrom((source) => source.value?.content)),
  forMember(
    (target) => target.published,
    mapFrom((source) => source.id.replace(/(\d{4})_(\d{2})_(\d{2})_(\d{2})(\d{2})(\d{2})/, '$1/$2/$3 $4:$5')))
);

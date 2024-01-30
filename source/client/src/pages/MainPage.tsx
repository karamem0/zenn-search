//
// Copyright (c) 2023-2024 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/zenn-search/blob/main/LICENSE
//

import React from 'react';

import { searchIndex } from '../services/SearchService';
import { Event } from '../types/Event';
import { SearchIndexData } from '../types/Model';

import Presenter from './MainPage.presenter';

function MainPage() {

  const [ error, setError ] = React.useState<boolean>();
  const [ indexes, setIndexes ] = React.useState<SearchIndexData[]>();
  const [ loading, setLoading ] = React.useState<boolean>(false);
  const [ query, setQuery ] = React.useState<string>('');

  const handleInputChange = React.useCallback((_?: Event, data?: string) => {
    setQuery(data ?? '');
  }, []);

  const handleSubmit = React.useCallback(async (event?: Event) => {
    event?.preventDefault();
    if (query.length === 0) {
      return;
    }
    try {
      setLoading(true);
      setError(false);
      setIndexes(await searchIndex(query));
    } catch (error) {
      console.error(error);
      setError(true);
    } finally {
      setLoading(false);
    }
  }, [
    query
  ]);

  return (
    <Presenter
      error={error}
      indexes={indexes}
      loading={loading}
      onInputChange={handleInputChange}
      onSubmit={handleSubmit} />
  );

}

export default MainPage;

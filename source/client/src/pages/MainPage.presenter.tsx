//
// Copyright (c) 2023-2024 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/zenn-search/blob/main/LICENSE
//

import React from 'react';

import { css } from '@emotion/react';

import ErrorSection from '../components/ErrorSection';
import Header from '../components/Header';
import IndexSection from '../components/IndexSection';
import LandingSection from '../components/LandingSection.presenter';
import LoadingSection from '../components/LoadingSection.presenter';
import { EventHandler } from '../types/Event';
import { SearchIndexData } from '../types/Model';

interface MainPageProps {
  error?: boolean,
  indexes?: SearchIndexData[],
  loading?: boolean,
  onInputChange: EventHandler<string>,
  onSubmit: EventHandler
}

function MainPage(props: Readonly<MainPageProps>) {

  const {
    error,
    loading,
    indexes,
    onInputChange,
    onSubmit
  } = props;

  return (
    <div
      css={css`
        display: flex;
        flex-flow: column;
      `}>
      <Header
        onInputChange={onInputChange}
        onSubmit={onSubmit} />
      {
        (() => {
          if (loading) {
            return (
              <LoadingSection />
            );
          }
          if (error) {
            return (
              <ErrorSection />
            );
          }
          if (indexes == null) {
            return (
              <LandingSection />
            );
          }
          return (
            <IndexSection indexes={indexes} />
          );
        })()
      }
    </div>
  );

}

export default MainPage;

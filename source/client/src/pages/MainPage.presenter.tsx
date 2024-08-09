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
import Footer from '../components/Footer';
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
  onDropdownSelect?: EventHandler<string>,
  onInputChange?: EventHandler<string>,
  onSubmit?: EventHandler
}

function MainPage(props: Readonly<MainPageProps>) {

  const {
    error,
    loading,
    indexes,
    onDropdownSelect,
    onInputChange,
    onSubmit
  } = props;

  return (
    <div
      css={css`
        display: grid;
        @media all and (width <= 960px) {
          grid-template-rows: 7.5rem 1fr 3rem;
          grid-template-columns: 1fr;
        }
        @media not all and (width <= 960px) {
          grid-template-rows: 3rem 1fr 3rem;
          grid-template-columns: 1fr;
        }
      `}>
      <Header
        onDropdownSelect={onDropdownSelect}
        onInputChange={onInputChange}
        onSubmit={onSubmit} />
      <div
        css={css`
          overflow-y: auto;
          @media all and (width <= 960px) {
            height: calc(100svh - 10.5rem);
          }
          @media not all and (width <= 960px) {
            height: calc(100svh - 6rem);
          }
        `}>
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
      <Footer />
    </div>
  );

}

export default React.memo(MainPage);

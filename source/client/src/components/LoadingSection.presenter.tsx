//
// Copyright (c) 2023-2024 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/zenn-search/blob/main/LICENSE
//

import React from 'react';

import { css } from '@emotion/react';
import { Spinner } from '@fluentui/react-components';

function LoadingSection() {

  return (
    <section
      css={css`
      display: grid;
      height: calc(100vh - 5rem);
      @media (width >= 960px) {
        height: calc(100vh - 3rem);
      }
    `}>
      <Spinner />
    </section>
  );

}

export default LoadingSection;

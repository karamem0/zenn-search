//
// Copyright (c) 2023-2024 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/zenn-search/blob/main/LICENSE
//

import React from 'react';

import { Spinner } from '@fluentui/react-components';

import { css } from '@emotion/react';

function LoadingSection() {

  return (
    <section
      css={css`
        display: grid;
      `}>
      <Spinner />
    </section>
  );

}

export default React.memo(LoadingSection);

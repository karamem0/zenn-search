//
// Copyright (c) 2023-2024 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/zenn-search/blob/main/LICENSE
//

import React from 'react';

import { Caption1, Subtitle1 } from '@fluentui/react-components';

import { css } from '@emotion/react';

function ErrorSection() {

  return (
    <section
      css={css`
        display: grid;
        align-items: center;
        justify-content: center;
        height: calc(100vh - 3rem);
      `}>
      <div
        css={css`
          display: grid;
          grid-template-rows: auto;
          grid-template-columns: 1fr;
          grid-gap: 1rem;
        `}>
        <Subtitle1
          css={css`
            text-align: center;
          `}>
          エラーが発生しました
        </Subtitle1>
        <Caption1
          css={css`
            text-align: center;
          `}>
          すみませんが時間をおいて再度お試しください。
        </Caption1>
      </div>
    </section>
  );

}

export default React.memo(ErrorSection);

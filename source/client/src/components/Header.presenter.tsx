//
// Copyright (c) 2023-2024 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/zenn-search/blob/main/LICENSE
//

import React from 'react';

import { SearchBox, Text } from '@fluentui/react-components';

import { css } from '@emotion/react';

import { useTheme } from '../providers/ThemeProvider';
import { EventHandler } from '../types/Event';

import TargetDropdown from './TargetDropdown';

interface HeaderProps {
  onDropdownSelect?: EventHandler<string>,
  onInputChange?: EventHandler<string>,
  onSubmit?: EventHandler
}

function Header(props: Readonly<HeaderProps>) {

  const {
    onDropdownSelect,
    onInputChange,
    onSubmit
  } = props;

  const { theme } = useTheme();

  return (
    <header
      css={css`
        display: grid;
        grid-gap: 0.5rem;
        align-items: center;
        justify-content: center;
        padding: 0.5rem 1rem;
        background-color: ${theme.colorBrandBackground};
        @media all and (width <= 960px) {
          grid-template-rows: 1.5rem 2rem 2rem;
          grid-template-columns: 1fr auto;
        }
        @media not all and (width <= 960px) {
          grid-template-rows: 2rem;
          grid-template-columns: 1fr 1fr 1fr;
        }
      `}>
      <Text
        as="h1"
        css={css`
          overflow: hidden;
          font-size: 1rem;
          font-weight: ${theme.fontWeightBold};
          line-height: calc(1rem * 1.25);
          color: ${theme.colorNeutralBackground1};
          text-overflow: ellipsis;
          white-space: nowrap;
          @media all and (width <= 960px) {
            grid-row: 1 / 2;
            grid-column: 1 / 2;
          }
          @media not all and (width <= 960px) {
            grid-row: 1 / 2;
            grid-column: 1 / 2;
            max-width: 20rem;
          }
        `}>
        からめもぶろぐ。記事検索
      </Text>
      <form
        css={css`
          display: flex;
          flex-direction: column;
          align-items: center;
          justify-content: center;
          @media all and (width <= 960px) {
            grid-row: 3 / 4;
            grid-column: 1 / 3;
          }
          @media not all and (width <= 960px) {
            grid-row: 1 / 2;
            grid-column: 2 / 3;
          }
        `}
        onSubmit={onSubmit}>
        <SearchBox
          css={css`
            width: 100%;
            @media all and (width <= 960px) {
              max-width: 100%;
            }
            @media not all and (width <= 960px) {
              max-width: 40rem;
            }
          `}
          onChange={(event, data) => onInputChange?.(event, data.value)} />
      </form>
      <div
        css={css`
          display: flex;
          flex-direction: row;
          align-items: center;
          justify-content: end;
          @media all and (width <= 960px) {
            grid-row: 2 / 3;
            grid-column: 1 / 3;
          }
          @media not all and (width <= 960px) {
            grid-row: 1 / 2;
            grid-column: 3 / 4;
          }
        `}>
        <TargetDropdown onSelect={onDropdownSelect} />
      </div>
    </header>
  );

}

export default React.memo(Header);

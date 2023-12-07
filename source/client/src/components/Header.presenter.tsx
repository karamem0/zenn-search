//
// Copyright (c) 2023 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/zenn-search/blob/main/LICENSE
//

import React from 'react';

import { css } from '@emotion/react';
import {
  Button,
  Input,
  Link,
  Text
} from '@fluentui/react-components';
import { GitHubLogoIcon, SearchIcon } from '@fluentui/react-icons-mdl2';

import { useTheme } from '../providers/ThemeProvider';
import { EventHandler } from '../types/Event';

interface HeaderProps {
  onInputChange?: EventHandler<string>,
  onSubmit?: EventHandler
}

function Header(props: Readonly<HeaderProps>) {

  const {
    onInputChange,
    onSubmit
  } = props;

  const { theme } = useTheme();

  return (
    <header
      css={css`
        display: grid;
        grid-template-rows: auto auto;
        grid-template-columns: 1fr auto;
        grid-gap: 0.5rem;
        align-items: center;
        justify-content: center;
        height: 5rem;
        padding: 0.5rem 1rem;
        background-color: ${theme.colorBrandBackground};
        @media (width >= 960px) {
          grid-template-rows: auto;
          grid-template-columns: 1fr 1fr 1fr;
          height: 3rem;
          padding: 0 1rem;
        }
      `}>
      <Text
        as="h1"
        css={css`
          grid-row: 1 / 2;
          grid-column: 1 / 2;
          overflow: hidden;
          font-size: 1rem;
          font-weight: ${theme.fontWeightBold};
          line-height: calc(1rem * 1.25);
          color: ${theme.colorNeutralBackground1};
          text-overflow: ellipsis;
          white-space: nowrap;
          @media (width >= 960px) {
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
          grid-row: 2 / 3;
          grid-column: 1 / 3;
          align-items: center;
          justify-content: center;
          @media (width >= 960px) {
            grid-row: 1 / 2;
            grid-column: 2 / 3;
          }
        `}
        onSubmit={onSubmit}>
        <Input
          contentAfter={(
            <Button
              appearance="transparent"
              size="small"
              type="submit"
              icon={(
                <SearchIcon
                  css={css`
                  width: 1rem;
                  height: 1rem;
                  line-height: 1rem;
                `} />
              )} />
          )}
          css={css`
            width: 100%;
            @media (width >= 960px) {
              max-width: 40rem;
            }
          `}
          onChange={(event, data) => onInputChange?.(event, data.value)} />
      </form>
      <div
        css={css`
          display: grid;
          grid-row: 1 / 2;
          grid-column: 2 / 3;
          align-items: center;
          justify-content: end;
          @media (width >= 960px) {
            grid-row: 1 / 2;
            grid-column: 3 / 4;
          }
        `}>
        <Link
          as="a"
          href="https://github.com/karamem0/zenn-search"
          target="_blank"
          css={css`
            color: ${theme.colorNeutralBackground1};
            &:active {
              color: ${theme.colorNeutralBackground1Pressed};
            }
            &:hover {
              color: ${theme.colorNeutralBackground1Hover};
            }
          `}>
          <GitHubLogoIcon
            css={css`
              width: 1.5rem;
              height: 1.5rem;
              line-height: 1.5rem;
            `} />
        </Link>
      </div>
    </header>
  );

}

export default Header;

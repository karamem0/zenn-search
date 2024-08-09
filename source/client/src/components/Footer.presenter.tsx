//
// Copyright (c) 2023-2024 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/zenn-search/blob/main/LICENSE
//

import React from 'react';

import { Link, Text } from '@fluentui/react-components';
import { GitHubLogoIcon } from '@fluentui/react-icons-mdl2';

import { css } from '@emotion/react';

import { useTheme } from '../providers/ThemeProvider';

function Footer() {

  const { theme } = useTheme();

  return (
    <footer
      css={css`
        display: grid;
        align-items: center;
        justify-content: center;
      `}>
      <Link
        as="a"
        href="https://github.com/karamem0/zenn-search"
        target="_blank"
        css={css`
          display: inline-flex;
          flex-direction: row;
          grid-gap: 0.25rem;
          align-items: center;
          justify-content: center;
          color: ${theme.colorNeutralForeground1};
          &:active {
            color: ${theme.colorNeutralForeground1Pressed};
          }
          &:hover {
            color: ${theme.colorNeutralForeground1Hover};
          }
        `}>
        <GitHubLogoIcon
          css={css`
            font-size: 1rem;
            line-height: 1rem;
          `} />
        <Text
          css={css`
            font-size: 0.875rem;
            line-height: calc(0.875rem * 1.25);
          `}>
          GitHub
        </Text>
      </Link>
    </footer>
  );

}

export default Footer;

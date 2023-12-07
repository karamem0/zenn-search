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
  Caption1,
  Card,
  Text
} from '@fluentui/react-components';

import { useTheme } from '../providers/ThemeProvider';
import { SearchIndexData } from '../types/Model';

interface IndexSectionProps {
  indexes: SearchIndexData[]
}

function IndexSection(props: Readonly<IndexSectionProps>) {

  const {
    indexes
  } = props;

  const { theme } = useTheme();

  return (
    <section
      css={css`
        display: grid;
        grid-template-rows: auto;
        grid-template-columns: repeat(auto-fill, minmax(20rem, 1fr));
        grid-gap: 1rem;
        padding: 1rem;
      `}>
      {
        indexes.map((index) => (
          <Card key={index.id}>
            <div
              css={css`
                display: grid;
                grid-template-rows: auto auto auto auto;
                grid-template-columns: auto 1fr;
                grid-gap: 0.5rem;
              `}>
              <div
                css={css`
                  display: grid;
                  grid-row: 1 / 3;
                  grid-column: 1 / 2;
                `}>
                <Text
                  title={index.emoji}
                  css={css`
                    overflow: hidden;
                    font-size: 2rem;
                    font-weight: ${theme.fontWeightSemibold};
                    line-height: calc(2rem * 1.25);
                    text-overflow: ellipsis;
                    white-space: nowrap;
                  `}>
                  {index.emoji ?? '📝'}
                </Text>
              </div>
              <div
                css={css`
                  display: grid;
                  grid-row: 1 / 2;
                  grid-column: 2 / 3;
                `}>
                <Text
                  title={index.title}
                  css={css`
                    overflow: hidden;
                    font-size: 1rem;
                    font-weight: ${theme.fontWeightSemibold};
                    line-height: calc(1rem * 1.25);
                    text-overflow: ellipsis;
                    white-space: nowrap;
                  `}>
                  {index.title ?? '-'}
                </Text>
              </div>
              <Caption1
                as="p"
                css={css`
                  grid-row: 2 / 3;
                  grid-column: 2 / 3;
                  font-size: 0.75rem;
                  line-height: calc(0.75rem * 1.25);
                `}>
                {index.published}
              </Caption1>
              <Text
                as="p"
                css={css`
                  position: relative;
                  grid-row: 3 / 4;
                  grid-column: 1 / 3;
                  height: calc(3rem * 1.25);
                  overflow: hidden;
                `}>
                {index.content}
              </Text>
              <Button
                appearance="primary"
                as="a"
                href={`${import.meta.env.VITE_ZENN_URL}/articles/${index.id}`}
                shape="circular"
                target="_blank"
                css={css`
                  grid-row: 4 / 5;
                  grid-column: 1 / 3;
                `}>
                記事を読む
              </Button>
            </div>
          </Card>
        ))
      }
    </section>
  );

}

export default IndexSection;

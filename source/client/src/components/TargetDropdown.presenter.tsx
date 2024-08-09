//
// Copyright (c) 2023-2024 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/zenn-search/blob/main/LICENSE
//

import React from 'react';

import { Dropdown, Option } from '@fluentui/react-components';

import { css } from '@emotion/react';

import { EventHandler } from '../types/Event';
import { TargetDropdownOption } from '../types/Option';

interface TargetDropdownProps {
  defaultOption: TargetDropdownOption,
  options: TargetDropdownOption[],
  selectedOption: TargetDropdownOption,
  title?: string,
  onSelect?: EventHandler<TargetDropdownOption>
}

function TargetDropdown(props: Readonly<TargetDropdownProps>) {

  const {
    defaultOption,
    options,
    selectedOption,
    title,
    onSelect
  } = props;

  return (
    <Dropdown
      aria-label={title}
      defaultSelectedOptions={[ defaultOption.text ]}
      defaultValue={defaultOption.value}
      multiselect={false}
      title={title}
      button={(
        <span
          css={css`
            overflow: hidden;
            text-overflow: ellipsis;
            white-space: nowrap;
          `}>
          {selectedOption?.text}
        </span>
      )}
      css={css`
        width: 100%;
        @media not all and (width <= 960px) {
          max-width: 18rem;
        }
      `}
      onOptionSelect={(event, data) =>
        onSelect?.(
          event,
          {
            text: data.optionText ?? '',
            value: data.optionValue ?? ''
          })}>
      {
        options?.map((option) => (
          <Option
            key={option.value}
            value={option.value}>
            {option.text}
          </Option>
        ))
      }
    </Dropdown>
  );

}

export default React.memo(TargetDropdown);

//
// Copyright (c) 2023-2024 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/zenn-search/blob/main/LICENSE
//

import React from 'react';

import { Event, EventHandler } from '../types/Event';
import { SearchTarget } from '../types/Model';

import Presenter from './TargetDropdown.presenter';

interface TargetDropdownOption {
  text: string,
  value: string
}

interface TargetDropdownProps {
  onSelect?: EventHandler<string>
}

function TargetDropdown(props: Readonly<TargetDropdownProps>) {

  const {
    onSelect
  } = props;

  const options = React.useMemo(() => [
    {
      text: '(指定しない)',
      value: SearchTarget.both
    },
    {
      text: 'Azure AI Search (HNSW)',
      value: 'aisearch'
    },
    {
      text: 'Azure Cosmos DB for MongoDB (IVF)',
      value: 'mongodb'
    }
  ], []);

  const [ selectedOption, setSelectedOption ] = React.useState(options[0]);

  const handleSelect = React.useCallback((event?: Event, value?: TargetDropdownOption) => {
    setSelectedOption(value ?? options[0]);
    onSelect?.(event, value?.value ?? options[0].value);
  }, [
    options,
    onSelect
  ]);

  return (
    <Presenter
      defaultOption={options[0]}
      options={options}
      selectedOption={selectedOption}
      title='データソース'
      onSelect={handleSelect} />
  );

}

export default TargetDropdown;

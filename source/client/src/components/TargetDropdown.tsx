//
// Copyright (c) 2023-2024 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/zenn-search/blob/main/LICENSE
//

import React from 'react';

import { Event, EventHandler } from '../types/Event';
import { TargetDropdownOption, targetDropdownOptions } from '../types/Option';

import Presenter from './TargetDropdown.presenter';

interface TargetDropdownProps {
  onSelect?: EventHandler<string>
}

function TargetDropdown(props: Readonly<TargetDropdownProps>) {

  const {
    onSelect
  } = props;

  const [ selectedOption, setSelectedOption ] = React.useState(targetDropdownOptions[0]);

  const handleSelect = React.useCallback((event?: Event, value?: TargetDropdownOption) => {
    setSelectedOption(value ?? targetDropdownOptions[0]);
    onSelect?.(event, value?.value ?? targetDropdownOptions[0].value);
  }, [
    onSelect
  ]);

  return (
    <Presenter
      defaultOption={targetDropdownOptions[0]}
      options={targetDropdownOptions}
      selectedOption={selectedOption}
      title='データソース'
      onSelect={handleSelect} />
  );

}

export default TargetDropdown;

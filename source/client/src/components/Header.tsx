//
// Copyright (c) 2023-2024 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/zenn-search/blob/main/LICENSE
//

import React from 'react';

import { EventHandler } from '../types/Event';

import Presenter from './Header.presenter';

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

  return (
    <Presenter
      onDropdownSelect={onDropdownSelect}
      onInputChange={onInputChange}
      onSubmit={onSubmit} />
  );

}

export default Header;

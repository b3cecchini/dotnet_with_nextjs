import React from 'react'
import Search from './Search'
import Logo from './Logo'

export default function NavBar() {
  return (
    <header className="sticky top-0 z-50 flex justify-center space-x-40 bg-white-600 shadow-sm text-slate-900 ">
        <Logo />
        <div className="py-5 px-8"> Left</div>
        <div className="py-5 px-8"> <Search /></div>
        <div className="py-5 px-8"> Right</div>
    </header>
  )
}

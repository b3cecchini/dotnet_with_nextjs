import React from 'react'
import Search from './Search'
import Logo from './Logo'
import LoginButton from './LoginButton'
import { getCurrentUser } from '../Actions/AuthActions'
import UserActions from './UserActions'

export default async function NavBar() {
  const user = await getCurrentUser()

  return (
    <header className="sticky top-0 z-50 flex justify-between items-center bg-white-600 shadow-md text-slate-900 py-3 ">
        <Logo />

        <Search />
        {user ? (
          <UserActions user={user} />
        ) : (
        <LoginButton />
        )}
        
    </header>
  )
}

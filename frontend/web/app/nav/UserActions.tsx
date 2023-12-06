'use client'

import { Button, DropdownDivider } from 'flowbite-react'
import Link from 'next/link'
import React from 'react'
import { Dropdown } from 'flowbite-react'
import { signOut } from 'next-auth/react'
import {User} from "next-auth"
import {HiCog, HiUser} from 'react-icons/hi2'
import { AiFillCar, AiFillTrophy, AiOutlineLogout } from 'react-icons/ai'

type Props = {
    user: Partial<User>
}
export default function UserActions({user}: Props) {
    return (
        <Dropdown inline label={`Welcome ${user.name}`}>
            <Dropdown.Item icon={HiUser}>
                <Link href={'/'}>
                    My Auctions
                </Link>
            </Dropdown.Item>
            <Dropdown.Item icon={AiFillTrophy}>
                <Link href={'/'}>
                    Auctions won
                </Link>
            </Dropdown.Item>
            <Dropdown.Item icon={AiFillCar}>
                <Link href={'/'}>
                    Create Auction
                </Link>
            </Dropdown.Item>
            <Dropdown.Item icon={HiCog}>
                <Link href={'/session'}>
                   Session
                </Link>
            </Dropdown.Item>

            <DropdownDivider />

            <Dropdown.Item icon={AiOutlineLogout} onClick={() => signOut({callbackUrl: '/'})}>
                Sign Out
            </Dropdown.Item>
        </Dropdown>
    )
}

'use client'

import { useParamsStore } from '@/hooks/useParamsStore'
import { Dropdown, DropdownDivider, DropdownItem } from 'flowbite-react'
import { User } from 'next-auth'
import { signOut } from 'next-auth/react'
import Link from 'next/link'
import { usePathname, useRouter } from 'next/navigation'
import React from 'react'
import { AiFillCar, AiFillTrophy, AiOutlineLogout } from 'react-icons/ai'
import { HiCog, HiUser } from 'react-icons/hi2'

type Props = 
{
    user : User
}

export default function UserActions({user}: Props) {
    const router = useRouter();
    const pathName = usePathname();
    const setParams = useParamsStore(state => state.setParams);

    function setWinner()
    {
        setParams({winner: user.username, seller: undefined});
        if(pathName !== '/')
        {
            router.push('/');
        }
    }

    function setSeller()
    {
        setParams({winner: undefined, seller: user.username});
        if(pathName !== '/')
        {
            router.push('/');
        }
    }
  return (
    <Dropdown inline label={`Welcome ${user.name}`} className='cursor-pointer'>
        <DropdownItem icon={HiUser} onClick={() => setSeller()}>
 
            My Auctions

        </DropdownItem>
        <DropdownItem icon={AiFillTrophy} onClick={() => setWinner()}>
            Auctions Won
        </DropdownItem>
        <DropdownItem icon={AiFillCar}>
            <Link href='/auctions/create'>
                Sell My Car
            </Link>
        </DropdownItem>
        <DropdownItem icon={HiCog}>
            <Link href='/session'>
                Session(dev only)
            </Link>
        </DropdownItem>
        <DropdownDivider />
        <DropdownItem icon={AiOutlineLogout} onClick={() => signOut({callbackUrl: '/'})}>
            Sign Out
        </DropdownItem>
    </Dropdown>
  )
}

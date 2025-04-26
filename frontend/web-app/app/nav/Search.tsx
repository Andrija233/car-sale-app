'use client'

import { useParamsStore } from '@/hooks/useParamsStore'
import { usePathname, useRouter } from 'next/navigation';
import React from 'react'
import { FaSearch } from 'react-icons/fa'

export default function Search() {
    const router = useRouter();
    const pathName = usePathname();
    const setParams = useParamsStore(state => state.setParams);
    const setSearchValue = useParamsStore(state => state.setSearchValue);
    const searchValue = useParamsStore(state => state.searchValue);

    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    function onChange(event: any)
    {
        setSearchValue(event.target.value);
    }

    function changeParams ()
    {
      if(pathName !== '/') router.push('/');
      setParams({searchTerm: searchValue});
    }
  return (
    <div className='flex w-[50%] items-center border-2 rounded-full py-2 shadow-sm'>
      <input 
        type="text"
        value={searchValue}
        placeholder='Search for cars by make model or color'
        className='flex-grow pl-5 bg-transparent focus:outline-none border-transparent focus:border-transparent focus:ring-0 text-sm text-gray-600 '
        onChange={onChange}
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        onKeyDown={(e: any) => e.key === 'Enter' && changeParams()}
      />
      <button onClick={changeParams}>
        <FaSearch size={34} className='bg-red-400 text-white p-2 rounded-full cursor-pointer mx-2'/>
      </button>
    </div>
  )
}

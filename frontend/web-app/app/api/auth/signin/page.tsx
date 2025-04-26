export const dynamic = 'force-dynamic';

import EmptyFilter from '@/app/components/EmptyFilter'
import React from 'react'

type SignInProps = {
  searchParams?: Promise<{
    callbackUrl?: string | undefined | null;
  }> | undefined;
};

export default async function SignIn({ searchParams }: SignInProps) {
  const searchParamsValue = await searchParams;
  const callbackUrl = searchParamsValue?.callbackUrl;

  if (!callbackUrl) {
    throw new Error('Callback URL is missing');
  }

  return (
    <EmptyFilter 
      title="You need to be logged in!"
      subtitle="You need to be logged in to view this page"
      showLogin
      callbackUrl={callbackUrl}
    />
  );
}
